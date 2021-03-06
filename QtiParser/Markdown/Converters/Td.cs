using HtmlAgilityPack;

namespace AssessmentExportParser.Markdown.Converters
{
	public class Td: ConverterBase
	{
		public Td(Converter converter)
			: base(converter)
		{
			Converter.Register("td", this);
			Converter.Register("th", this);
		}

		public override string Convert(HtmlNode node)
		{
			string content = TreatChildren(node);
			return string.Format(" {0} |", content);
		}
	}
}
