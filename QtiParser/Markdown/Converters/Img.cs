using HtmlAgilityPack;

namespace AssessmentExportParser.Markdown.Converters
{
	public class Img: ConverterBase
	{
		public Img(Converter converter)
			: base(converter)
		{
			Converter.Register("img", this);
		}

		public override string Convert(HtmlNode node)
		{
			string alt = node.GetAttributeValue("alt", string.Empty);
			string src = node.GetAttributeValue("src", string.Empty);
			string title = ExtractTitle(node);

			title = title.Length > 0 ? string.Format(" \"{0}\"", title) : "";

			return string.Format("![{0}]({1}{2})", alt, src, title);
		}
	}
}
