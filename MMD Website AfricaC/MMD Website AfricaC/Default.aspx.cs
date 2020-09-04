using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MMD_Website_AfricaC
    {
    public partial class _Default : Page
        {
        public bool isDeveloper = false;
        public bool inAfrica = false;
        public bool isAfrica = false;
        public bool inGPHC = false;
        public bool isGPHC = false;
        public bool isUK = false;
        public bool inUK = false;
        public bool isIndia = false;
        public bool inIndia = false;
        public bool isAustralia = false;
        public bool inAustralia = false;
        public bool isMauritius = false;
        public bool inMauritius = false;
        public bool isGMSAfrica = false;
        public bool inGMSAfrica = false;
        public string userName = "";
        public string localIP = "";
        protected void Page_Load(object sender, EventArgs e)
            {
            userName = Request.ServerVariables["LOGON_USER"];
            localIP = IPNetworking.GetIP4Address();

            if (userName.ToLower().Contains("warwick.mclean") || userName.ToLower().Contains("warwick") || userName.ToLower().Contains("ian.wakely"))
                {
                isDeveloper = true;
                }

            if (userName.ToLower().Contains("sheree") || userName.ToLower().Contains("willem"))
                {
                isMauritius = true;
                }

            if (userName.ToLower().Contains("mmdafrica")) { isAfrica = true; }
            if (localIP.ToLower().Contains("10.3.0.")) { inAfrica = true; }
            if (localIP.ToLower().Contains("10.3.1.")) { inAfrica = true; }
            if (userName.ToLower().Contains("mmdafrica")) { isGMSAfrica = true; }

            if (userName.ToLower().Contains("mmdgms")) { isGMSAfrica = true; }
            if (localIP.ToLower().Contains("10.3.10.")) { inGMSAfrica = true; }

            if (userName.ToLower().Contains("mmdgphc")) { isGPHC = true; }
            if (localIP.ToLower().Contains("10.1.0.")) { inGPHC = true; }

            if (userName.ToLower().Contains("mmdgphc")) { isMauritius = true; }
            if (localIP.ToLower().Contains("10.7.1.")) { inMauritius = true; }

            if (userName.ToLower().Contains("mmdsizers")) { isUK = true; }
            if (localIP.ToLower().Contains("192.29.220.")) { inUK = true; }

            lbllocalIP.Text = localIP;

            Page.DataBind();
            }

        public class IPNetworking
            {
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

        public void OpenIE(string url)
            {
            System.Diagnostics.Process.Start("iexplore", url);
            }
        }
    }