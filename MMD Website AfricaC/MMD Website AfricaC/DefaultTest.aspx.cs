using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MMD_Website_AfricaC
    {
    public partial class DefaultTest : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
            {
            string userName = Request.ServerVariables["LOGON_USER"];
            string localIP = GetIpAddress();

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
            if (userName.ToLower().Contains("mmdafrica")) { isGMSAfrica = true; }

            if (userName.ToLower().Contains("mmdgms")) { isGMSAfrica = true; }
            if (localIP.ToLower().Contains("10.3.10.")) { inGMSAfrica = true; }

            if (userName.ToLower().Contains("mmdgphc")) { isGPHC = true; }
            if (localIP.ToLower().Contains("10.1.0.")) { inGPHC = true; }

            if (userName.ToLower().Contains("mmdgphc")) { isMauritius = true; }
            if (localIP.ToLower().Contains("10.7.1.")) { inMauritius = true; }

            if (userName.ToLower().Contains("mmdsizers")) { isUK = true; }
            if (localIP.ToLower().Contains("192.29.220.")) { inUK = true; }

            Page.DataBind();
            }

        protected string GetIpAddress()
            {
            string IpAddress = "";
            HttpContext context = HttpContext.Current;
            string sIPaddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(sIPaddress))
                {
                IpAddress = context.Request.ServerVariables["REMOTE_ADDR"];
                }
            else
                {
                IpAddress = sIPaddress;
                }

            return IpAddress;
            }
        }
    }