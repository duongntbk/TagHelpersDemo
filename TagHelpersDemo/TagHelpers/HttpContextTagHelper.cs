using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TagHelpersDemo.TagHelpers
{
    [HtmlTargetElement("http-context")]
    public class HttpContextTagHelper : TagHelper
    {
        private IHttpContextAccessor _httpContextAccessor;

        public HttpContextTagHelper(IHttpContextAccessor httpContextAccessor) =>
            _httpContextAccessor = httpContextAccessor;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            foreach (var kvp in _httpContextAccessor.HttpContext.Request.Query)
            {
                var pair = new TagBuilder("span");
                pair.InnerHtml.AppendHtml($"{kvp.Key}: {kvp.Value}");
                output.Content.AppendHtml(pair);
                output.Content.AppendHtml("<br/>");
            }
        }
    }
}
