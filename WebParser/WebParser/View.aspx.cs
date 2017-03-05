using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebParser
{
  public partial class View : Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      Processor.GetPageFromSource("http://skay.ua/4-samsung/");
    }
  }
}