using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TagHelpersDemo.TagHelpers
{
    [HtmlTargetElement("hello-world")]
    public class HelloWorldTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            var hello = new TagBuilder("span");
            hello.InnerHtml.AppendHtml("Hello,");
            output.Content.AppendHtml(hello);

            var world = new TagBuilder("a");
            world.InnerHtml.AppendHtml("World!");
            world.Attributes.Add("href", "https://example.com");
            output.Content.AppendHtml(world);
        }
    }
}
