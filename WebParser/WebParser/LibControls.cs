using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebParser
{
  public class LibControls
  {
    public static HtmlGenericControl CreateHtmlControl(string tagName, string className, Control parent = null,
      string innerHtml = null, string id = null)
    {
      var control = new HtmlGenericControl(tagName);
      control.Attributes.Add("class", className);
      if (innerHtml != null)
        control.InnerHtml = innerHtml;
      if (id != null)
        control.ID = id;
      if (parent != null)
        parent.Controls.Add(control);
      return control;
    }

    public static Button CreateButton(string id, string cssClass, string text, Control parent)
    {
      var button = new Button
      {
        ID = id,
        CssClass = cssClass,
        Text = text
      };
      parent.Controls.Add(button);
      return button;
    }
  }
}