using MailCommunicationUtils.Data;
using MailCommunicationUtils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace MailCommunicationUtils.Classes
{
    public class SMTPMailMessage
    {
        private SmtpClient smtpClient { get; set; }
        public SMTPMailMessage(string SMTPName, string SMTPPassword, string SMTPHost)
        {
            smtpClient = new SmtpClient();
            smtpClient.Credentials = new NetworkCredential(SMTPName, SMTPPassword);
            smtpClient.Host = SMTPHost;
            smtpClient.Port = 587;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
        }
        public MailMessage CreateMail(string ReceiverAdress, string htmlContent, string subject)
        {
            var from = new MailAddress("jelle.dispersyn@student.vives.be", "R.Decaestecker - Vives");
            var to = new MailAddress(ReceiverAdress);
            var message = new MailMessage(from, to);
            message.Subject = subject;
            message.Body = htmlContent;
            message.IsBodyHtml = true;

            return message;
        }

        public MailMessage CreateMail(string ReceiverAdress, string htmlContent, string subject, string attatchmentPath1, string attatchmentPath2)
        {
            var from = new MailAddress("jelle.dispersyn@student.vives.be", "R.Decaestecker - Vives");
            var to = new MailAddress(ReceiverAdress);
            var message = new MailMessage(from, to);
            Attachment data1 = new Attachment(attatchmentPath1, MediaTypeNames.Application.Octet);
            Attachment data2 = new Attachment(attatchmentPath2, MediaTypeNames.Application.Octet);
            message.Attachments.Add(data1);
            message.Attachments.Add(data2);
            message.Subject = subject;
            message.Body = htmlContent;
            message.IsBodyHtml = true;

            return message;
        }

        public MailResult sendMessage(MailMessage msg)
        {
            var result = new MailResult() { Status = MailSendingStatus.OK };

            try
            {
                smtpClient.Send(msg);
            }
            catch (Exception e)
            {
                result.Status = MailSendingStatus.HasError;
                result.Message = e.Message;
            }
            return result;
        }
    }
}
