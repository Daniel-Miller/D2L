using HtmlAgilityPack;

namespace AssessmentExportParser.Markdown.Converters
{
	public class Ignore: ConverterBase
	{
		public Ignore(Converter converter)
			: base(converter)
		{
			Converter.Register("colgroup", this);
			Converter.Register("col", this);
		}

		public override string Convert(HtmlNode node)
		{
			return "";
		}
	}
}
