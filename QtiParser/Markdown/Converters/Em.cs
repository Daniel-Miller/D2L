using System.Linq;

using HtmlAgilityPack;

namespace AssessmentExportParser.Markdown.Converters
{
	public class Em: ConverterBase
	{
		public Em(Converter converter)
			: base(converter)
		{
			Converter.Register("em", this);
			Converter.Register("i", this);
		}

		public override string Convert(HtmlNode node)
		{
			string content = TreatChildren(node);
			if (string.IsNullOrEmpty(content.Trim()) || AlreadyItalic(node))
			{
				return content;
			}
			else
			{
				return "*" + content.Trim() + "*";
			}
		}

		private bool AlreadyItalic(HtmlNode node)
		{
			return node.Ancestors("i").Count() > 0 || node.Ancestors("em").Count() > 0;
		}
	}
}
