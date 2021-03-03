using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using SitefinityWebApp.Mvc.Models;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;

namespace SitefinityWebApp.Extensions
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MailService : IMailService
    {
        #region Public members

        public string DefaultSenderEmail;
        public string DefaultSenderName;

        #endregion

        #region Email Service

        public MailService()
        {
            DefaultSenderEmail = Config.Get<SystemConfig>().SmtpSettings.DefaultSenderEmailAddress;
            DefaultSenderName = "My website";
        }

        public bool Send(string fromAddress, string toAddresses, string subject, string messageHtml,
            List<Attachment> attachments = null, string fromName = "", string bccAdresses = "", string replyTo = "")
        {
            var isSentToRecepient = false;

            MailMessage message = new MailMessage
            {
                Subject = subject,
                Body = messageHtml,
                IsBodyHtml = true,
                BodyEncoding = Encoding.UTF8
            };

            // Add sender's address
            try
            {
                message.From = string.IsNullOrEmpty(fromName)
                    ? new MailAddress(fromAddress)
                    : new MailAddress(fromAddress, fromName);
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                message.From = new MailAddress(DefaultSenderEmail, DefaultSenderName);
            }

            // Add addresses of recipients
            try
            {
                message.To.Add(toAddresses);
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                return false;
            }

            // Add bcc addresses
            if (!string.IsNullOrEmpty(bccAdresses))
            {
                try
                {
                    message.Bcc.Add(bccAdresses);
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                }
            }

            // Add reply to addresses
            try
            {
                if (!string.IsNullOrEmpty(replyTo))
                {
                    message.ReplyToList.Add(new MailAddress(replyTo));
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }

            // Add attachments 
            if (attachments != null)
            {
                foreach (Attachment file in attachments)
                {
                    message.Attachments.Add(file);
                }
            }

            SmtpClient smtpClient = new SmtpClient();
            SmtpElement settings = Config.Get<SystemConfig>().SmtpSettings;
            var basicCredential = new NetworkCredential(settings.UserName, settings.Password, settings.Domain);
            smtpClient.Host = settings.Host;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = basicCredential;

            // Attempts to send the email
            try
            {
                smtpClient.Send(message);
                isSentToRecepient = true;
            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }

            return isSentToRecepient;
        }

        private string GetTemplate(string filePath)
        {
            string template;
            string physicalPath;
            try
            {
                physicalPath = HttpContext.Current.Server.MapPath(filePath);
            }
            catch (Exception ex)
            {
                Log.Write(ex, ConfigurationPolicy.ErrorLog);
                return string.Empty;
            }

            try
            {
                using (StreamReader reader = new StreamReader(physicalPath))
                {
                    template = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex, ConfigurationPolicy.ErrorLog);
                return string.Empty;
            }

            return template;
        }

        #endregion

        public bool SendLeaseContactFormNotification(LeaseContactFormModel leaseContact, string emailSubject, string recipients)
        {
            bool isSent = true;

            // TODO: implement

            return isSent;
        }
    }
}