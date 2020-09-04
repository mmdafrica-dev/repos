using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkshopAssistant
{
    public partial class MainForm : Form
    {
        OracleConnection c_mmdonline = new OracleConnection(ConfigurationManager.ConnectionStrings["CS_mmdonline"].ConnectionString);
        OracleConnection c_mtms = new OracleConnection(ConfigurationManager.ConnectionStrings["CS_mtms"].ConnectionString);
        DataTable Activity = new DataTable();
        DataTable WOPR_DATA = new DataTable();
        public static int UserId { get; set; }
        public static string UserName { get; set; }
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var loginForm = new Login();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                LoggedIn();
            }
        }

        private void LoggedIn()
        {
            //Build User Information
            UserName_label.Text = "Welcome: " + UserName;
            //Check where user is
            using (OracleCommand cmd = new OracleCommand("SELECT * FROM MMD_WS_ACTIVITY WHERE user_id = " + UserId + " and to_date(currentdate, 'dd/MON/YY') = to_date(SYSDATE, 'dd/MON/YY') ", c_mmdonline))
            {
                //this.Label2.Text = "SELECT * FROM word_data left join wopr_data on wopr1_co_site = word1_co_site and woprstatus < 80 and wopr1_ord_ref = word1_ref WHERE word1_co_site = 'SA' AND wordstatus < 22 and wopr2_work in (" + Mopr_list + ")";
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                oda.Fill(Activity);
            }
            if(Activity.Rows.Count > 0)
            {
                //User has something Clocked against his name for today
                foreach (DataRow row in Activity.Rows)
                {
                    //Check if user is on Site or on Leave or Sick
                    if (row["activity"].ToString() == "Site")
                    {
                        //Run Site Setup
                        break;
                    }
                    if (row["activity"].ToString() == "Sick")
                    {
                        //Run Sick Setup
                        break;
                    }
                    if (row["activity"].ToString() == "On Leave")
                    {
                        //Run On Leave Setup
                        break;
                    }


                }
            }
            else
            {
                //User has nothing Clocked against his name for today
                SetupUser(UserId);
            }

        }
        private void SetupUser(int userid)
        {
            string UserCategories = "";

            using (OracleCommand cmd = new OracleCommand("Select * from MMD_G_STAFF where id = " + userid, c_mmdonline))
            {
                c_mmdonline.Open();
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    UserCategories = reader["category"].ToString();
                }
                c_mmdonline.Close();
            }

            if (UserCategories.IndexOf("Site") >= 0) { btn_Site.Visible = true; } else { btn_Site.Visible = false; };
            string Mopr_list = "";
            if (UserCategories.IndexOf("Fitter") >= 0 && UserCategories.IndexOf("Artisan") >= 0) { Mopr_list += "'004','007',"; };
            if (UserCategories.IndexOf("Fitter") >= 0 && UserCategories.IndexOf("Assistant") >= 0) { Mopr_list += "'104','107',"; };
            if (UserCategories.IndexOf("Machinist") >= 0 && UserCategories.IndexOf("Artisan") >= 0) { Mopr_list += "'003','510','511','512','513','514','515','516','517','518','519','520',"; };
            if (UserCategories.IndexOf("Machinist") >= 0 && UserCategories.IndexOf("Assistant") >= 0) { Mopr_list += "'103','510','511','512','513','514','515','516','517','518','519','520',"; };
            if (UserCategories.IndexOf("Welder") >= 0 && UserCategories.IndexOf("Artisan") >= 0) { Mopr_list += "'002','007',"; };
            if (UserCategories.IndexOf("Welder") >= 0 && UserCategories.IndexOf("Assistant") >= 0) { Mopr_list += "'102','107',"; };
            if (UserCategories.IndexOf("Cleaner") >= 0 && UserCategories.IndexOf("Assistant") < 0) { Mopr_list += "'012',"; };
            if (UserCategories.IndexOf("Cleaner") >= 0 && UserCategories.IndexOf("Assistant") >= 0) { Mopr_list += "'112',"; };
            if (UserCategories.IndexOf("Painter") >= 0) { Mopr_list += "'005','105',"; };
            if (UserCategories.IndexOf("Programmer") >= 0) { Mopr_list += "'019',"; };

            Mopr_list += "'000'";

            using (OracleCommand cmd = new OracleCommand("SELECT * FROM word_data left join wopr_data on wopr1_co_site = word1_co_site and woprstatus < 80 and wopr1_ord_ref = word1_ref WHERE word1_co_site = 'SA' AND wordstatus < 22 and wopr2_work in (" + Mopr_list + ")", c_mtms))
            {
                //this.Label2.Text = "SELECT * FROM word_data left join wopr_data on wopr1_co_site = word1_co_site and woprstatus < 80 and wopr1_ord_ref = word1_ref WHERE word1_co_site = 'SA' AND wordstatus < 22 and wopr2_work in (" + Mopr_list + ")";
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                oda.Fill(WOPR_DATA);
                int xlocation = 10;
                int ylocation = 10;
                //pnl_WorkList.VerticalScroll.Visible = false;
                //string WorkOrderList = "";
                foreach (DataRow row in WOPR_DATA.Rows)
                {
                    //WorkOrderList += "<div class='ButtonBox' ><input type='button' class='btn btn-mmd' value='" + row["word1_ref"].ToString() + "' onClick='btnWork_Click()' /></div>";
                    Button bn = new Button
                    {
                        Name = "btn_WorkItem",
                        Text = row["word1_ref"].ToString().Trim(),
                        Location = new System.Drawing.Point(xlocation, ylocation),
                        FlatStyle = FlatStyle.Flat,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Size = new System.Drawing.Size(180, 50)
                    };
                    bn.Font = new Font(bn.Font.FontFamily, 24);
                    bn.ForeColor = Color.FromArgb(254, 221, 0);
                    bn.BackColor = Color.FromArgb(49, 155, 66);
                    bn.Click += btnWork_Click;
                    pnl_WorkList.Controls.Add(bn);
                    ylocation += 65;
                }
            }
        }
        private void btnWork_Click(object sender, EventArgs e)
        {
            //do something with the button that is clicked
        }

        private void btn_Logout_Click(object sender, EventArgs e)
        {
            UserId = 0;
            UserName = "";
            pnl_WorkList.Controls.Clear();
            var loginForm = new Login();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                LoggedIn();
            }
        }

        private void btn_Site_Click(object sender, EventArgs e)
        {
            try
            {
                var queryString = string.Format(@"INSERT INTO MMD_WS_ACTIVITY (user_id, activity,time_start) VALUES (" + UserId + ",'SITE',SYSDATE)");
                OracleCommand cmd = c_mmdonline.CreateCommand();
                c_mmdonline.Open();
                cmd.CommandText = queryString;
                cmd.CommandType = CommandType.Text;
                int rowcount = cmd.ExecuteNonQuery();
                c_mmdonline.Close();
            }
            catch (Exception)
            {
                //MessageBox.Show("Something went wrong");
                throw;
            }
}

private void btn_Leave_Click(object sender, EventArgs e)
{

}

private void btn_Sick_Click(object sender, EventArgs e)
{

}

private void btn_Exit_Click(object sender, EventArgs e)
{
    UserId = 0;
    UserName = "";
    Application.Exit();
}



        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    this.Session["UserName"] = "";
        //    this.Session["UserId"] = "";
        //    Response.Redirect("Default.aspx");
        //}
        //
        //private void SetupUser(string userid)
        //{
        //    string UserCategories = "";
        //
        //    using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        //    {
        //        using (OracleCommand cmd = new OracleCommand("Select * from MMD_G_STAFF where id = " + userid, conn))
        //        {
        //            conn.Open();
        //            using (OracleDataReader reader = cmd.ExecuteReader())
        //            {
        //                reader.Read();
        //                UserCategories = reader["category"].ToString();
        //                this.Session["UserCategories"] = UserCategories;
        //                this.UserFunction.Text = UserCategories;
        //            }
        //        }
        //
        //    }
        //    using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["CS_mtms"].ConnectionString))
        //    {
        //        if (UserCategories.IndexOf("Site") >= 0) { this.btnSite.Visible = true; } else { this.btnSite.Visible = false; };
        //        string Mopr_list = "";
        //        if (UserCategories.IndexOf("Fitter") >= 0 && UserCategories.IndexOf("Artisan") >= 0) { Mopr_list += "'004','007',"; };
        //        if (UserCategories.IndexOf("Fitter") >= 0 && UserCategories.IndexOf("Assistant") >= 0) { Mopr_list += "'104','107',"; };
        //        if (UserCategories.IndexOf("Machinist") >= 0 && UserCategories.IndexOf("Artisan") >= 0) { Mopr_list += "'003','510','511','512','513','514','515','516','517','518','519','520',"; };
        //        if (UserCategories.IndexOf("Machinist") >= 0 && UserCategories.IndexOf("Assistant") >= 0) { Mopr_list += "'103','510','511','512','513','514','515','516','517','518','519','520',"; };
        //        if (UserCategories.IndexOf("Welder") >= 0 && UserCategories.IndexOf("Artisan") >= 0) { Mopr_list += "'002','007',"; };
        //        if (UserCategories.IndexOf("Welder") >= 0 && UserCategories.IndexOf("Assistant") >= 0) { Mopr_list += "'102','107',"; };
        //        if (UserCategories.IndexOf("Cleaner") >= 0 && UserCategories.IndexOf("Assistant") < 0) { Mopr_list += "'012',"; };
        //        if (UserCategories.IndexOf("Cleaner") >= 0 && UserCategories.IndexOf("Assistant") >= 0) { Mopr_list += "'112',"; };
        //        if (UserCategories.IndexOf("Painter") >= 0) { Mopr_list += "'005','105',"; };
        //        if (UserCategories.IndexOf("Programmer") >= 0) { Mopr_list += "'019',"; };
        //
        //        Mopr_list += "'000'";
        //
        //        using (OracleCommand cmd = new OracleCommand("SELECT * FROM word_data left join wopr_data on wopr1_co_site = word1_co_site and woprstatus < 80 and wopr1_ord_ref = word1_ref WHERE word1_co_site = 'SA' AND wordstatus < 22 and wopr2_work in (" + Mopr_list + ")", conn))
        //        {
        //            this.Label2.Text = "SELECT * FROM word_data left join wopr_data on wopr1_co_site = word1_co_site and woprstatus < 80 and wopr1_ord_ref = word1_ref WHERE word1_co_site = 'SA' AND wordstatus < 22 and wopr2_work in (" + Mopr_list + ")";
        //            OracleDataAdapter oda = new OracleDataAdapter(cmd);
        //            oda.Fill(WOPR_DATA);
        //
        //            //string WorkOrderList = "";
        //            foreach (DataRow row in WOPR_DATA.Rows)
        //            {
        //                //WorkOrderList += "<div class='ButtonBox' ><input type='button' class='btn btn-mmd' value='" + row["word1_ref"].ToString() + "' onClick='btnWork_Click()' /></div>";
        //                HtmlGenericControl NewDiv = new
        //                HtmlGenericControl("div");
        //                NewDiv.Attributes.Add("class", "ButtonBox");
        //                Button bn = new Button();
        //                bn.CssClass = "btn btn-mmd";
        //                bn.Text = row["word1_ref"].ToString().Trim();
        //                bn.Click += btnWork_Click;
        //                NewDiv.Controls.Add(bn);
        //                WorkOrder_List.Controls.Add(NewDiv);
        //            }
        //            //this.WorkOrder_List.InnerHtml = WorkOrderList;
        //        }
        //    }
        //}
        //protected void btnWork_Click(object sender, EventArgs e)
        //{
        //    foreach (DataRow row in WOPR_DATA.Rows)
        //    {
        //        Button b = sender as Button;
        //        if (row["word1_ref"].ToString().Trim() == b.Text)
        //        {
        //            //build the page from here
        //            this.Label3.Text = row["word2_contract"].ToString() + " : " + row["word1_ref"].ToString() + " - " + row["wordpart"].ToString() + " " + row["wopr2_no"].ToString() + " " + row["woprdesc"].ToString();
        //        }
        //    }
        //}
    }
}
