using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MMD_Website_AfricaC.Mods.Extras
{
    public partial class notices : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["SelectedMenu"] = 1;
        }
    }
}