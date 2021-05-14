using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace WroseModel.Helper
{
    public class EmailHelper
    {
        string smtpClient = ConfigurationManager.AppSettings["smtpClient"];
        string smtpPort = ConfigurationManager.AppSettings["smtpPort"];
        string fromEmailAddress = ConfigurationManager.AppSettings["fromEmailAddress"];
        string fromEmailPassword = ConfigurationManager.AppSettings["fromEmailPassword"];
        public void SendMail(string recipient, string firstname)
        {
            SmtpClient client = new SmtpClient(smtpClient);

            client.Port = Convert.ToInt32(smtpPort);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials =
            new System.Net.NetworkCredential(fromEmailAddress, fromEmailPassword);
            client.EnableSsl = true;
            client.Credentials = credentials;

            try
            {
                var mail = new MailMessage(fromEmailAddress.Trim(), recipient.Trim());
                mail.Subject = "Registration successful";
                mail.Body = "Dear " + firstname + ",<br/><br/>Congratulation!<br/><br/>You have registered succesfully.<br/><br/><br/><br/>Thank you.<br/>Test mail.";
                mail.IsBodyHtml = true;
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}