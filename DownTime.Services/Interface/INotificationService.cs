using DownTime.CommonModel.Models;

namespace DownTime.Services.Interface
{
    public interface INotificationService
    {
        /// <summary>
        /// Sends email
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool EmailSend(EmailSendModel model);
        /// <summary>
        /// Sends Sms
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool SmsSend(EmailSendModel model);
        /// <summary>
        /// Sends Push
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool PushSend(EmailSendModel model);
    }
}
