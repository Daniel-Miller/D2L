using System;
using System.Collections.Generic;
using System.Linq;

using HtmlAgilityPack;

namespace AssessmentExportParser.Markdown.Converters
{
	public class Tr: ConverterBase
	{
		public Tr(Converter converter)
			: base(converter)
		{
			Converter.Register("tr", this);
		}

		public override string Convert(HtmlNode node)
		{
			string content = TreatChildren(node).TrimEnd();

			string result = string.Format("|{0}{1}", content, Environment.NewLine);

			return IsTableHeaderRow(node) ? result + UnderlineFor(node) : result;
		}

		private bool IsTableHeaderRow(HtmlNode node)
		{
			return node.ChildNodes.FindFirst("th")!=null;
		}

		private string UnderlineFor(HtmlNode node)
		{
			int colCount = node.ChildNodes.Count();

			List<string> cols = new List<string>();

			for (int i = 0; i < colCount; i++ )
			{
				cols.Add("---");
			}

			return "| " + cols.Aggregate((item1,item2) => item1 + " | " + item2) + " |" + Environment.NewLine;

			 
		}
	}
}
