using System;
using System.Collections.Generic;
using System.Linq;

using AssessmentExportParser.Markdown.Converters;

using HtmlAgilityPack;

namespace AssessmentExportParser.Markdown
{
	public class Converter
	{
		private IDictionary<string, IConverter> _converters = new Dictionary<string, IConverter>();
		private Config _config;

		public Converter():this(new Config())
		{
		}

		public Converter(Config config)
		{
			_config = config;

			foreach (Type ctype in System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IConverter)) && !t.IsAbstract))
			{
				Activator.CreateInstance(ctype, this);
			}
		}

		public Config Config 
		{
			get { return _config; }
		}

		public string Convert(string html)
		{
			var cleaner = new Cleaner();

			html = cleaner.PreTidy(html);

			HtmlDocument doc = new HtmlDocument();
			doc.LoadHtml(html);

			var root = doc.DocumentNode;

			string result = Lookup(root.Name).Convert(root);

			return result;
		}

		public void Register(string tagName, IConverter converter)
		{
			_converters.Add(tagName, converter);
		}

		public void Unregister(string tagName)
		{
			_converters.Remove(tagName);
		}

		public IConverter Lookup(string tagName)
		{
			return _converters.ContainsKey(tagName) ? _converters[tagName] : GetDefaultConverter(tagName);
		}

		protected IConverter GetDefaultConverter(string tagName)
		{
			switch (_config.UnknownTagsConverter)
			{
				case "pass_through":
					return new PassThrough(this);
				case "drop":
					return new Drop(this);
				case "bypass":
					return new ByPass(this);
				default:
					throw new Exception(string.Format("Unknown tag: {0}", tagName));
			}
		}
	}
}
