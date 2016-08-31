using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TooksCms.Core.Interfaces;

namespace TooksCms.Core.Mail
{
    public class Emailer
    {
        private MailAddress _sender = null;
        //private string _recipient = string.Empty;
        //private string _displayName = string.Empty;
        private List<MailAddress> _recipients;
        private string _subject = string.Empty;
        private string _message = string.Empty;
        private string _smtpServer = string.Empty;

        /// <summary>
        /// public property for the html setting, defautls to true as this was previous hardcoded
        /// </summary>
        public bool IsBodyHtml = true;

        /// <summary>
        /// Creates a new email ready to send.
        /// </summary>
        /// <param name="Sender">string email address of the sender</param>
        /// <param name="Recipients">comma delimited string of recipient email addresses</param>
        /// <param name="Subject">Subject of the mail</param>
        /// <param name="Message">text content of the mail.</param>
        public Emailer(MailAddress sender, MailAddress recipient, string subject, string message, string smtpServer)
        {
            _sender = sender;
            _recipients = new List<MailAddress> { recipient };
            _subject = subject;
            _message = message;
            _smtpServer = smtpServer;
        }

        public Emailer(MailAddress sender, IEnumerable<MailAddress> recipients, string subject, string message, string smtpServer)
        {
            _sender = sender;
            _recipients = recipients.Select(r => new MailAddress(r.Address, r.DisplayName)).ToList();
            _subject = subject;
            _message = message;
            _smtpServer = smtpServer;
        }

        public void Send()
        {
            Send(null);
        }

        public void Send(List<Attachment> attachments)
        {
            try
            {
                using (MailMessage msg = new MailMessage())
                {
                    msg.IsBodyHtml = this.IsBodyHtml;
                    msg.From = _sender;
                    _recipients.ForEach(r => msg.To.Add(r));
                    msg.Subject = _subject;
                    msg.Body = _message;

                    if (attachments != null) { attachments.ForEach(atch_ => msg.Attachments.Add(atch_)); }

                    using (SmtpClient client = new SmtpClient(_smtpServer))
                    {
                        client.Send(msg);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Mail Sending Failed", ex);
            }
        }

        #region static methods

        public static void SendMail(MailAddress sender, IEnumerable<MailAddress> recipients, string subject, string message, string smtpServer)
        {
            try
            {
                Emailer mail = new Emailer(sender, recipients, subject, message, smtpServer);
                mail.Send();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static void SendMail(MailAddress sender, IEnumerable<MailAddress> recipients, string subject, string message, string smtpServer, List<Attachment> attachments)
        {
            try
            {
                Emailer mail = new Emailer(sender, recipients, subject, message, smtpServer);
                mail.Send(attachments);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static void SendMail(MailAddress sender, MailAddress recipient, string subject, string message, string smtpServer)
        {
            try
            {
                Emailer mail = new Emailer(sender, recipient, subject, message, smtpServer);
                mail.Send();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static void SendMail(MailAddress sender, MailAddress recipient, string subject, string message, string smtpServer, List<Attachment> attachments)
        {
            try
            {
                Emailer mail = new Emailer(sender, recipient, subject, message, smtpServer);
                mail.Send(attachments);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion
    }
}
