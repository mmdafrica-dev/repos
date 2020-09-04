using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace MMD_Website_AfricaC.Classes
{
    public class MailMessages
    {
        public bool SendMessage(string to, string cc, string bcc, string subject, string body, MailPriority priority = MailPriority.Normal)
        {
            bool response = false;
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("mtms.admin@mmdafrica.co.za", "MMD Admin");
                mail.To.Add(new MailAddress(to));
                if (cc.Length > 0)
                {
                    mail.CC.Add(new MailAddress(cc));
                }
                if (bcc.Length > 0)
                {
                    mail.Bcc.Add(new MailAddress(bcc));
                }
                mail.Subject = subject;
                mail.Priority = priority;
                mail.Body = body;

                SmtpClient smtpClient = new SmtpClient("10.3.0.3", 25);

                smtpClient.Credentials = new System.Net.NetworkCredential("mtms.admin@mmdafrica.co.za", "Pass123word");
                smtpClient.UseDefaultCredentials = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;

                try
                {
                    smtpClient.Send(mail);
                    response = true;
                }
                catch (Exception)
                {
                    response = false;
                    throw;
                }
            }

            return response;
        }
    }


}