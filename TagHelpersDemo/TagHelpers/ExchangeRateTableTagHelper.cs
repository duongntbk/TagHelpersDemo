using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NodaTime;

namespace TagHelpersDemo.TagHelpers
{
    [HtmlTargetElement("exchange-rate-table")]
    public class ExchangeRateTableTagHelper : TagHelper
    {
        private readonly IClock _clock;

        [HtmlAttributeName("include-time-stamp")]
        public bool IncludeTimeStamp { get; set; }

        public ExchangeRateTableTagHelper(IClock clock) => _clock = clock;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "table";
            output.TagMode = TagMode.StartTagAndEndTag;

            if (IncludeTimeStamp)
            {
                AddCaptionSection(output);
            }

            AddHeaderSection(output);
            await AddBodySection(output);
        }

        private void AddCaptionSection(TagHelperOutput output)
        {
            var captionSection = new TagBuilder("caption");
            var now = _clock.GetCurrentInstant().ToDateTimeUtc();
            captionSection.InnerHtml.AppendHtml($"This exchange rate was retrieved at: {now} UTC");
            output.Content.AppendHtml(captionSection);
        }

        private void AddHeaderSection(TagHelperOutput output)
        {
            var headerSection = new TagBuilder("thead");
            var headerRow = new TagBuilder("tr");
            AddCellToRow(headerRow, "Source");
            AddCellToRow(headerRow, "Target");
            AddCellToRow(headerRow, "Rate");
            headerSection.InnerHtml.AppendHtml(headerRow);
            output.Content.AppendHtml(headerSection);
        }

        private static async Task AddBodySection(TagHelperOutput output)
        {
            var bodySection = new TagBuilder("tbody");
            var childContent = await output.GetChildContentAsync();
            bodySection.InnerHtml.AppendHtml(childContent);
            output.Content.AppendHtml(bodySection);
        }

        private void AddCellToRow(TagBuilder row, string cellValue)
        {
            var cell = new TagBuilder("td");
            cell.InnerHtml.AppendHtml(cellValue);
            row.InnerHtml.AppendHtml(cell);
        }
    }
}
