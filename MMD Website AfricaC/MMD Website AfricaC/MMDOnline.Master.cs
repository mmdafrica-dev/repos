using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MMD_Website_AfricaC.Models;
using Oracle.ManagedDataAccess.Client;

namespace MMD_Website_AfricaC
{
    public partial class MMDOnline : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] == null)
                {
                    //Code when initial loading
                    string logonUser = Request.ServerVariables["LOGON_USER"];
                    int padLocation = logonUser.IndexOf("\\");

                    string hsDomain = logonUser.Substring(0, padLocation).ToLower();
                    string hsUsername = logonUser.Substring(padLocation + 1).ToLower();
                    DataTable table = new DataTable();
                    string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["mmdonline"].ConnectionString;
                    OracleConnection conn = new OracleConnection(strCon);

                    string sql = "Select * from MMD_G_STAFF where enabled = 1 and lower(domain) = '" + hsDomain + "' and lower(username) = '" + hsUsername + "' ";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(table);
                    DataView view = new DataView(table);
                    foreach (DataRowView row in view)
                    {
                        Session["dashoard"] = row["dashboard"];
                        Session["username"] = row["firstname"] + " " + row["surname"];
                        Session["userid"] = row["id"];
                        Session["seclev"] = Int32.Parse(row["security"].ToString());
                        Session["domain"] = row["domain"];
                        Session["MTMSSection"] = row["MTMSSection"];
                        Session["Section"] = row["Section"];
                        Session["function"] = row["function"];
                        Session["email"] = row["email"];
                    }
                }
                string profileLink = "<a href='/mods/extras/profile.asp?id=" + Session["userid"] + "' ><img alt='Profile Image' src='/images/staff/" + Session["userid"] + ".jpg' class='userimage' /></a>";
                userdata.Controls.Clear();
                userdata.Controls.Add(new LiteralControl(profileLink));
                GetMenu();
                getSubMenu();
            }
            else
            {

            }

        }

        private void GetMenu()
        {
            DataTable table = new DataTable();
            string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["mmdonline"].ConnectionString;
            OracleConnection conn = new OracleConnection(strCon);

            string sql = "Select * from MMD_OL_MENU where menulevel = 0 and enable = 1 and id in (Select menuid from mmd_OL_ACCESS where userid = " + Session["userid"] + " and ENABLEACCESS = 1) order by priority";
            OracleCommand cmd = new OracleCommand(sql, conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.Fill(table);
            DataView view = new DataView(table);

            string MenuString = "";
            MenuString += "<ul class='toolbar'>";
            MenuString += "<li id='tbarToggleNorth' class='first'><span></span></li>";
            foreach (DataRowView row in view)
            {
                MenuString += "<li onclick=\"UpdateMenu('" + row["id"] + "','" + row["menu"] + "')\" >" + row["menu"] + "</ li >";
            }
            MenuString += "</ul>";
            topMenu.Controls.Clear();
            topMenu.Controls.Add(new LiteralControl(MenuString));
        }

        private void getSubMenu()
        {
            DataTable table = new DataTable();
            string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["mmdonline"].ConnectionString;
            using (OracleConnection conn = new OracleConnection(strCon))
            {
                //Open the connection to the database
                conn.Open();
                string sql = "SELECT a.ID,a.MENULEVEL,a.LINKID,a.MENU,a.URL,a.PRIORITY,connect_by_isleaf node, (Select count(b.id) from MMD_OL_MENU b where b.enable = 1 AND b.linkid = a.id AND b.id IN (SELECT menuid FROM mmd_OL_ACCESS WHERE userid = " + Session["userid"] + " AND ENABLEACCESS = 1)) SubNodes from MMD_OL_MENU a where a.enable = 1 AND a.id IN (SELECT menuid FROM mmd_OL_ACCESS WHERE userid = " + Session["userid"] + " AND ENABLEACCESS = 1) START WITH a.MENULEVEL = 0 connect by prior a.id = a.linkid order SIBLINGS by a.priority";
                OracleCommand cmd = new OracleCommand(sql);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                using (OracleDataAdapter dataAdapter = new OracleDataAdapter())
                {
                    dataAdapter.SelectCommand = cmd;
                    dataAdapter.Fill(table);
                }
            }
            DataView view = new DataView(table);
            DataView subview1 = new DataView(table);
            DataView subview2 = new DataView(table);
            DataView subview3 = new DataView(table);

            string MenuString = "";
            int DefMenu = 1;
            if (Session["SelectedMenu"] != null)
            {
                DefMenu = int.Parse(Session["SelectedMenu"].ToString());
            }
            view.RowFilter = "menulevel = 0";
            foreach (DataRowView row in view)
            {
                //check the session variable and set the menu accordingly
                
                if (int.Parse(row["id"].ToString()) == DefMenu)
                {
                    MenuString += "<div id='menu" + row["id"] + "' class='sidebar' style='display:block;' >";
                }
                else
                {
                    MenuString += "<div id='menu" + row["id"] + "' class='sidebar' style='display:none;' >";
                }
                if (int.Parse(row["subnodes"].ToString()) > 0)
                {
                    subview1.RowFilter = "linkid = " + row["id"];
                    MenuString += "<ul class='topnav' >";
                    foreach (DataRowView subrow1 in subview1)
                    {
                        MenuString += "<li>";
                        if (int.Parse(subrow1["subnodes"].ToString()) > 0)
                        {
                            subview2.RowFilter = "linkid = " + subrow1["id"];
                            MenuString += "<a>" + subrow1["menu"] + "</a>";
                            MenuString += "<ul >";
                            foreach (DataRowView subrow2 in subview2)
                            {
                                MenuString += "<li>";
                                if (int.Parse(subrow2["subnodes"].ToString()) > 0)
                                {
                                    subview3.RowFilter = "linkid = " + subrow2["id"];
                                    MenuString += "<a>" + subrow2["menu"] + "</a>";
                                    MenuString += "<ul >";
                                    foreach (DataRowView subrow3 in subview3)
                                    {
                                        MenuString += "<li>";
                                        MenuString += "<a href=\"" + subrow3["url"].ToString().Replace(".asp", ".aspx") + "\" >" + subrow3["menu"] + "</a>";
                                        MenuString += "</li>";
                                    }
                                    MenuString += "</ul>";

                                }
                                else
                                {
                                    MenuString += "<a href=\"" + subrow2["url"].ToString().Replace(".asp", ".aspx") + "\" >" + subrow2["menu"] + "</a>";
                                }
                                MenuString += "</li>";
                            }
                            MenuString += "</ul>";
                        }
                        else
                        {
                            MenuString += "<a href=\"" + subrow1["url"].ToString().Replace(".asp", ".aspx") + "\" >" + subrow1["menu"] + "</a>";
                        }
                        MenuString += "</li>";
                    }
                    MenuString += "</ul>";
                }
                MenuString += "</div>";
            }
            leftmenu.Controls.Clear();
            leftmenu.Controls.Add(new LiteralControl(MenuString));
        }
    }
}