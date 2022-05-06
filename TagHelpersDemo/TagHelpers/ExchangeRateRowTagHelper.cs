using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using TagHelpersDemo.Repository;

namespace TagHelpersDemo.TagHelpers
{
    [HtmlTargetElement("tr", ParentTag = "exchange-rate-table")]
    public class ExchangeRateRowTagHelper : TagHelper
    {
        private readonly IExchangeRateRepository _exchangeRateRepository;

        [HtmlAttributeName("source")]
        public string SourceText { get; set; }

        [HtmlAttributeName("target")]
        public string TargetText { get; set; }

        public ExchangeRateRowTagHelper(IExchangeRateRepository exchangeRateRepository) =>
            _exchangeRateRepository = exchangeRateRepository;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (!Enum.TryParse(SourceText, out Currency source) || !Enum.TryParse(TargetText, out Currency target))
            {
                // Source or target currency is incorrect. Nothing to render.
                return;
            }

            var rateDto = await _exchangeRateRepository.RetrieveAsync(source, target);

            AddCellToRow(output, SourceText);
            AddCellToRow(output, TargetText);
            AddCellToRow(output, $"{rateDto.Rate:0.0000}");
        }

        private void AddCellToRow(TagHelperOutput row, string cellValue)
        {
            var cell = new TagBuilder("td");
            cell.InnerHtml.AppendHtml(cellValue);
            row.Content.AppendHtml(cell);
        }
    }
}
