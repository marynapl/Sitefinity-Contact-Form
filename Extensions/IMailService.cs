using System.Collections.Generic;
using System.Net.Mail;
using SitefinityWebApp.Mvc.Models;

namespace SitefinityWebApp.Extensions
{
    public interface IMailService
    {
        bool Send(string fromAddress, string toAddresses, string subject, string messageHtml,
           List<Attachment> attachments = null, string fromName = "", string bccAdresses = "", string replyTo = "");

        bool SendLeaseContactFormNotification(LeaseContactFormModel leaseContact, string emailSubject,
            string recipients);
    }
}