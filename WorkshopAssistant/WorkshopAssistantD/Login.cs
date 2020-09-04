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
using Oracle.ManagedDataAccess;
using Oracle.ManagedDataAccess.Client;

namespace WorkshopAssistant
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            //label2.Text = Screen.PrimaryScreen.Bounds.Width + ":" + Screen.PrimaryScreen.Bounds.Height;

            OracleConnection conn = new OracleConnection(); conn.ConnectionString = ConfigurationManager.ConnectionStrings["CS_mmdonline"].ConnectionString;
            OracleCommand cmd = new OracleCommand("Select id, firstname || ' ' || surname FullName from MMD_G_STAFF where active = 1 and split = 'F' order by firstname, surname", conn);
            OracleDataAdapter oda = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            oda.Fill(ds);
            LoginSelect.DataSource = ds.Tables[0].DefaultView;
            LoginSelect.DisplayMember = "Fullname";
            LoginSelect.ValueMember = "id";

        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            //process Login Details
            MainForm.UserId = Int32.Parse(LoginSelect.SelectedValue.ToString());
            MainForm.UserName = LoginSelect.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void lbl_name_Click(object sender, EventArgs e)
            {

            }
        }
}
