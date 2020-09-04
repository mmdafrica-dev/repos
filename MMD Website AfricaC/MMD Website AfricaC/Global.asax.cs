using MMD_Website_AfricaC.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace MMD_Website_AfricaC
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        void Application_Error(object sender, EventArgs e)
        {

            //string ErrorDescription = Server.GetLastError().ToString();

            ////'Creation of event log if it does not exist  
            //string EventLogName = "MMD-Websites";
            //if (!EventLog.SourceExists(EventLogName))
            //{
            //    EventLog.CreateEventSource(EventLogName, EventLogName);
            //}
            ////' Inserting into event log
            //EventLog Log = new EventLog();
            //Log.Source = EventLogName;
            //Log.WriteEntry(ErrorDescription, EventLogEntryType.Error);

            //MailMessages message = new MailMessages();
            //string ErrorMessage = "The error description is as follows : " + Server.GetLastError().ToString();
            //message.SendMessage("warwick@mmdafrica.co.za", "", "", "Error in the Site", ErrorMessage, MailPriority.High);
        }
    }
}