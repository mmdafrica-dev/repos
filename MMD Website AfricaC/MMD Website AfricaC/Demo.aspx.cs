using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MMD_Website_AfricaC
{
    public partial class Demo : System.Web.UI.Page
    {
        public bool isDeveloper = false;
        public string UserFullName;
        public string userName;
        public string userDomain;
        public string localIP;

        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SiteDataCS"].ConnectionString);

        StringBuilder output = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            userName = Request.ServerVariables["LOGON_USER"];
            userDomain = userName.Substring(0, userName.LastIndexOf("\\"));
            localIP = GetIpAddress();
            localIP = localIP.Substring(0, localIP.LastIndexOf(".") + 1);

            string sql = "select * from users where username = '" + userName.Trim().ToLower() + "' ";
            using (SqlCommand comm = new SqlCommand(sql, conn))
            {
                conn.Open();
                var dataReader = comm.ExecuteReader();
                while (dataReader.Read())
                {
                    isDeveloper = Int16.Parse(dataReader.GetValue(3).ToString()) == 1 ? true : false;
                    UserFullName = dataReader.GetValue(2).ToString();
                }
                conn.Close();
            }

            Page.DataBind();

            if (!IsPostBack)
            {
                output.Clear();
                GetMenuData();
                BuildTabData();
            }

        }
        private void GetMenuData()
        {
            DataTable table = new DataTable();
            string sql = "select * from offices order by officePriority";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(table);
            DataView view = new DataView(table);

            foreach (DataRowView row in view)
            {
                if (row["OfficeDomain"].ToString().ToLower().Trim() == userDomain.ToLower().Trim())
                {
                    output.Append("<li class='nav-item active'>");
                    output.Append("<a class='nav-link' href='#" + row["OfficeTab"].ToString().Trim() + "' role='tab' data-toggle='tab'>" + row["OfficeName"].ToString().Trim() + "</a>");
                    output.Append("</li>");
                }
                else
                {
                    output.Append("<li class='nav-item'>");
                    output.Append("<a class='nav-link' href='#" + row["OfficeTab"].ToString().Trim() + "' role='tab' data-toggle='tab'>" + row["OfficeName"].ToString().Trim() + "</a>");
                    output.Append("</li>");
                }

            }
            menuBar.InnerHtml = output.ToString();
            view.Dispose();
            da.Dispose();
            cmd.Dispose();
            table.Dispose();
        }

        private void BuildTabData()
        {
            DataTable table = new DataTable();
            SqlCommand cmd = new SqlCommand("select * from offices order by officePriority", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(table);
            DataView view = new DataView(table);

            foreach (DataRowView row in view)
            {
                HtmlGenericControl tabPane = new HtmlGenericControl("div");
                tabPane.Attributes.Add("id", row["OfficeTab"].ToString().Trim());
                if (row["OfficeDomain"].ToString().ToLower().Trim() == userDomain.ToLower().Trim())
                {
                    tabPane.Attributes.Add("class", "tab-pane active");
                }
                else
                {
                    tabPane.Attributes.Add("class", "tab-pane");
                }

                HtmlGenericControl panel1 = new HtmlGenericControl("div");
                panel1.Attributes.Add("class", "panel panel-mmd1");
                panel1.Attributes.Add("id", "");
                HtmlGenericControl panel1Heading = new HtmlGenericControl("div");
                panel1Heading.Attributes.Add("class", "panel-heading");
                panel1Heading.Attributes.Add("id", "");
                HtmlGenericControl Heading = new HtmlGenericControl("h3");
                Heading.Attributes.Add("class", "panel-title");
                Heading.InnerText = row["OfficeName"].ToString().Trim();
                Heading.Attributes.Add("id", "");
                panel1Heading.Controls.Add(Heading);

                HtmlGenericControl panel1Body = new HtmlGenericControl("div");
                panel1Body.Attributes.Add("class", "panel-body");

                DataTable table1 = new DataTable();
                SqlCommand cmd1 = new SqlCommand("select * from sites where office_id = " + Int32.Parse(row["officeId"].ToString()) + "order by sitePriority", conn);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                da1.Fill(table1);
                DataView view1 = new DataView(table1);

                //HtmlGenericControl btnGroup = new HtmlGenericControl("div");
                //btnGroup.Attributes.Add("id", "");
                //btnGroup.Attributes.Add("class", "btn-group");

                int rowCount = table1.Rows.Count;
                //Response.Write(rowCount + "<br />");
                foreach (DataRowView row1 in view1)
                {
                    HtmlGenericControl btnGroup_P = new HtmlGenericControl("p");
                    btnGroup_P.Attributes.Add("id", "");
                    HtmlGenericControl site = new HtmlGenericControl("a");
                    site.Attributes.Add("id", "");
                    site.Attributes.Add("href", row1["siteurl_home"].ToString());
                    site.Attributes.Add("target", "_blank");
                    site.Attributes.Add("class", "btn btn-default");
                    site.InnerText = row1["siteName"].ToString();
                    btnGroup_P.Controls.Add(site);
                    if (1 == 1)
                    {
                        panel1Body.Controls.Add(btnGroup_P);
                    }
                }
                //panel1Body.Controls.Add(btnGroup);


                panel1.Controls.Add(panel1Heading);
                panel1.Controls.Add(panel1Body);
                tabPane.Controls.Add(panel1);
                if (isDeveloper == true)
                {
                    HtmlGenericControl panel2 = new HtmlGenericControl("div");
                    panel2.Attributes.Add("class", "panel panel-mmd2");
                    HtmlGenericControl panel2Heading = new HtmlGenericControl("div");
                    panel2Heading.Attributes.Add("class", "panel-heading");
                    Heading = new HtmlGenericControl("h3");
                    Heading.Attributes.Add("class", "panel-title");
                    Heading.InnerText = row["OfficeName"].ToString().Trim() + " Developers";
                    Heading.Attributes.Add("id", "");
                    panel2Heading.Controls.Add(Heading);
                    HtmlGenericControl panel2Body = new HtmlGenericControl("div");
                    panel2Body.Attributes.Add("class", "panel-body");
                    panel2.Controls.Add(panel2Heading);
                    panel2.Controls.Add(panel2Body);

                    tabPane.Controls.Add(panel2);
                }
                tabContent.Controls.Add(tabPane);
                tabContent.Attributes.Add("id", "tabContent");
            }

            view.Dispose();
            da.Dispose();
            cmd.Dispose();
            table.Dispose();
            //< div id="tabs-global" class="tab-pane">
            //    <div class="panel panel-mmd1">
            //        <div class="panel-heading">
            //            <h3 class="panel-title">MMD Global</h3>
            //        </div>
            //        <div class="panel-body">                        
            //            <div class="btn-group">
            //                <p>
            //                    <a href = "http://10.1.0.23:8008" target="_blank" class="btn btn-default">MMD Corporate Portal internal</a>
            //                </p>
            //                <p>
            //                    <a href = "http://sales.mmdgphc.com" target="_blank" class="btn btn-default">MMD Corporate Portal external</a>
            //                </p>
            //            </div>
            //        </div>
            //    </div>
            //    <div class="panel panel-mmd2">
            //        <div class="panel-heading">
            //            <h3 class="panel-title">MMD Global Developers</h3>
            //        </div>
            //        <div class="panel-body">
            //            <div class="btn-group">
            //                <p>
            //                    <a href = "http://10.3.0.5:81" target="_blank" class="btn btn-default">Infor Portal</a>
            //                </p>
            //            </div>
            //            <div class="btn-group">
            //                <p>
            //                    <a href = "http://10.1.0.22:8080/tfs" target="_blank" class="btn btn-default">MMD TFS Server</a>
            //                </p>
            //            </div>
            //        </div>
            //    </div>
            //</div>

        }

        protected string GetIpAddress()
        {
            string IpAddress = "";
            var ip = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null
          && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != "")
         ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]
         : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (ip.Contains(","))
                ip = ip.Split(',').First();
            IpAddress = ip.Trim();

            return IpAddress;
        }

        protected int btnGroupCalc(int bG)
        {
            int btnGroup = 0;
            if (bG > 4)
            {
                if (bG > 6)
                {
                    if (bG > 8)
                    {

                    }
                    else
                    {
                        btnGroup = 2;
                    }
                }
                else
                {
                    btnGroup = 2;
                }
            }
            else
            {
                btnGroup = 1;
            }


            return btnGroup;
        }

    }
}