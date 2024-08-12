using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AdminPanel.TagHelpers;





[HtmlTargetElement("button", Attributes = "btn-action")]
public class ActionTagHelper : TagHelper
{
    public string BtnAction { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        (string color, string icon) btn = this.BtnAction switch
        {
            "delete" => (color: "danger", icon: "trash"),
            "edit" => (color: "warning", icon: "pen"),
            "create" => (color: "dark", icon: "save"),
            _ => (color: "primary", icon: "plus"),
        };


        output.TagName = "button";
        output.Attributes.SetAttribute("class", $"btn btn-outline-{btn.color}");
        output.Attributes.SetAttribute("type", "submit");
        output.Content.SetHtmlContent($"<i class='fas fa-{btn.icon}'></i> &nbsp {System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.BtnAction)}");


    }
}
