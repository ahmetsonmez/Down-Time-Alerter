using DownTime.CommonModel.Models;
using DownTime.Services.Interface;
using System;
using System.Net;
using System.Net.Mail;

namespace DownTime.Services.Concrete
{
    public class NotificationService : INotificationService
    {
        /// <summary>
        /// Sends email
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool EmailSend(EmailSendModel model)
        {
            SmtpClient client = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587
            };

            var credentials = new NetworkCredential("te8453044@gmail.com", "Abcd!123");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            var mailMessage =
                new MailMessage { From = new MailAddress("te8453044@gmail.com", model.DisplayName) };

            mailMessage.To.Add(model.To);
            mailMessage.Subject = model.Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = model.Body;

            try
            {
                client.Send(mailMessage);
                return true;                
            }
            catch (Exception)
            {
                               
            }

            return false;
        }
        /// <summary>
        /// Sends Sms
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SmsSend(EmailSendModel model)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Sends Push
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool PushSend(EmailSendModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}
