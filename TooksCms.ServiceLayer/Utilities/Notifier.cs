using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TooksCms.Core.Mail;
using TooksCms.ServiceLayer.Models;
using TooksCms.ServiceLayer.Support;

namespace TooksCms.ServiceLayer.Utilities
{
    public static class Notifier
    {
        public static void SendNotification(ContactFormModel model)
        {
            string template = GetEmailTemplate("TooksCms.ServiceLayer.EmailTemplates.ContactFormMail.txt");

            template = template.Replace("##NAME##", model.Name).Replace("##DATE##", model.Date.ToShortDateString() + " " + model.Date.ToShortTimeString()).Replace("##COMMENT##", model.Comment);

            SendNotification("Someone Has Contacted You | Digital Ectoplasm", template);
        }

        public static void SendNotification(CommentModel model)
        {
            string template = GetEmailTemplate("TooksCms.ServiceLayer.EmailTemplates.CommentMail.txt");

            template = template.Replace("##NAME##", model.Name).Replace("##COMMENT##", model.Comment).Replace("##ID##", model.ArticleId.ToString());

            SendNotification("Someone Commented on an Article | Digital Ectoplasm", template);
        }

        public static void SendNotification(string subject, string body)
        {

            string server = ConfigurationManager.AppSettings["mail:SmtpServer"];
            string sender = ConfigurationManager.AppSettings["mail:ReplyAddress"];
            string recipientEmail = ConfigurationManager.AppSettings["mail:NotificationRecipientEmail"];
            string recipientName = ConfigurationManager.AppSettings["mail:NotificationRecipientName"];

            try
            {
                Emailer.SendMail(new MailAddress(sender, "Digital Ectoplasm"), new MailAddress(recipientEmail, recipientName), subject, body, server);
            }
            catch (Exception ex)
            {
                Logger.LogException(Core.Enums.EventLogType.Error, "TooksCms.ServiceLayer", ex, "TooksCms.ServiceLayer.Utilities.SendNotification", 0);
            }
        }

        public static void SendReplyNotification(string subject, string body, MailAddress recipient)
        {

            string server = ConfigurationManager.AppSettings["mail:SmtpServer"];
            string sender = ConfigurationManager.AppSettings["mail:ReplyAddress"];

            try
            {
                Emailer.SendMail(new MailAddress(sender, "Digital Ectoplasm"), recipient, subject, body, server);
            }
            catch (Exception ex)
            {
                Logger.LogException(Core.Enums.EventLogType.Error, "TooksCms.ServiceLayer", ex, "TooksCms.ServiceLayer.Utilities.SendReplyNotification", 0);
            }
        }

        public static string GetEmailTemplate(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var textStreamReader = new StreamReader(assembly.GetManifestResourceStream(name)))
            {
                return textStreamReader.ReadToEnd();
            }
        }
    }
}
