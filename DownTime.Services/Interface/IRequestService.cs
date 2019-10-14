using DownTime.CommonModel.Models;

namespace DownTime.Services.Interface
{
    public interface IRequestService
    {
        /// <summary>
        /// Makes request to web site that comes from job. Sends notification and record it to  the db.
        /// </summary>
        /// <param name="model"></param>
        void Request(RequestModel model);
    }
}
