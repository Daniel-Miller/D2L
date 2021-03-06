using System;

using HtmlAgilityPack;

namespace AssessmentExportParser.Markdown.Converters
{
	public class Hr : ConverterBase
	{
		public Hr(Converter converter)
			: base(converter)
		{
			Converter.Register("hr", this);
		}

		public override string Convert(HtmlNode node)
		{
			return Environment.NewLine + "* * *" + Environment.NewLine;
		}
	}
}
