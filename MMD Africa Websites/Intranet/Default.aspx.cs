using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Intranet
    {
    public partial class _Default : Page
        {
        public string userName = "";
        public string localIP = "";
        protected void Page_Load(object sender, EventArgs e)
            {
            userName = HttpContext.Current.Request.ServerVariables["LOGON_USER"];
            localIP = IPNetworking.GetIP4Address();

            lblUserName.Text = userName;
            lbllocalIP.Text = localIP;

            Label1.Text = "GetXfor:" + IPNetworking.GetXfor();
            Label2.Text = "GetRemoteAddr:" + IPNetworking.GetRemoteAddr();
            Label3.Text = "UserHostAddress:" + HttpContext.Current.Request.UserHostAddress;
            Page.DataBind();
            }
        }

   
    public class IPNetworking
        {
        public static string GetXfor()
            {
            return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }

        public static string GetRemoteAddr()
            {
            return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

        public static string GetHostName()
            {
            string[] computer_name = Dns.GetHostEntry(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]).HostName.Split(new Char[] { '.' });
            return string.Join(", ", computer_name);
            }

        public static string GetHostAddr()
            {
            string IP4Address = String.Empty;
            string[] computer_name = Dns.GetHostEntry(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]).HostName.Split(new Char[] { '.' });
            //string[] ip_addresses = Dns.GetHostAddresses(computer_name[0].ToString());
            foreach (IPAddress IPA in Dns.GetHostAddresses(computer_name[0].ToString()))
                {
                IP4Address += IPA.ToString() + ";";
                }
            return IP4Address;
            }

        public static string GetIP4Address()
            {
            string IP4Address = String.Empty;
            if (string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
                {
                if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != "::1")
                    {
                    IP4Address = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    }
                }
            else
                {
                IP4Address = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }

            if (IP4Address != String.Empty)
                {
                return IP4Address;
                }
            string[] computer_name = Dns.GetHostEntry(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]).HostName.Split(new Char[] { '.' });

            foreach (IPAddress IPA in Dns.GetHostAddresses(computer_name[0].ToString()))
                {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                    {
                    IP4Address = IPA.ToString();
                    break;
                    }
                }

            if (IP4Address != String.Empty)
                {
                return IP4Address;
                }



            foreach (IPAddress IPA in Dns.GetHostAddresses(HttpContext.Current.Request.UserHostAddress))
                {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                    {
                    IP4Address = IPA.ToString();
                    break;
                    }
                }

            return IP4Address;
            }
        }

    }