using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using TagHelpersDemo.Repository;

namespace TagHelpersDemo.TagHelpers
{
    [HtmlTargetElement("all-exchange-rates", ParentTag = "exchange-rate-table")]
    public class AllExchangeRateRowsTagHelper : TagHelper
    {
        private readonly IExchangeRateRepository _exchangeRateRepository;

        [HtmlAttributeName("target")]
        public string TargetText { get; set; }

        public AllExchangeRateRowsTagHelper(IExchangeRateRepository exchangeRateRepository) =>
            _exchangeRateRepository = exchangeRateRepository;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (!Enum.TryParse(TargetText, out Currency target))
            {
                // Target currency is incorrect. Nothing to render.
                return;
            }

            var rateDtos = await _exchangeRateRepository.RetrieveAllAsync(target);

            foreach (var rateDto in rateDtos)
            {
                var row = new TagBuilder("tr");
                AddCellToRow(row, rateDto.SourceName);
                AddCellToRow(row, rateDto.TargetName);
                AddCellToRow(row, $"{rateDto.Rate:0.0000}");

                output.Content.AppendHtml(row);
            }
        }

        private void AddCellToRow(TagBuilder row, string cellValue)
        {
            var cell = new TagBuilder("td");
            cell.InnerHtml.AppendHtml(cellValue);
            row.InnerHtml.AppendHtml(cell);
        }
    }
}
