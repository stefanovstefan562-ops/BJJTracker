using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BjjTracker.TagHelpers
{
    [HtmlTargetElement("duration")]
    public class DurationTagHelper : TagHelper
    {
        public TimeSpan Value { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";

            int hours = (int)Value.TotalHours;
            int minutes = Value.Minutes;

            if (hours > 0 && minutes > 0)
            {
                output.Content.SetContent($"{hours}h {minutes}min");
            }
            else if (hours > 0)
            {
                output.Content.SetContent($"{hours}h");
            }
            else
            {
                output.Content.SetContent($"{minutes}min");
            }
        }
    }
}