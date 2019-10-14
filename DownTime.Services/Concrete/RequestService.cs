using DownTime.CommonModel.Models;
using DownTime.Services.Interface;
using System;
using System.Net.Http;

namespace DownTime.Services.Concrete
{

    public class RequestService : IRequestService
    {
        private readonly INotificationService _notificationService;
        private readonly IWebSiteRequestService _webSiteRequestService;
        
        public RequestService(INotificationService notificationService, IWebSiteRequestService requestService)
        {
            _notificationService = notificationService;
            _webSiteRequestService = requestService;
        }

        /// <summary>
        /// Makes request to web site that comes from job. Sends notification and record it to  the db.
        /// </summary>
        /// <param name="model"></param>
        public void Request(RequestModel model)
        {
            var result = Uri.TryCreate(model.Url, UriKind.Absolute, out var uriResult)
                          && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!result) return;

            using (var client = new HttpClient())
            using (var response = client.GetAsync(uriResult))
            {
                if (response.Result == null || response.Result.IsSuccessStatusCode) return;

                //send mail
                _notificationService.EmailSend(new EmailSendModel
                {
                    Subject = "There is an error at " + model.SiteName,
                    To = model.Email,
                    Body = model.Url + "=> Status Code : " + response.Result.StatusCode,
                    DisplayName = "DownSite Alerter"
                });

                //add db
                _webSiteRequestService.CreateOrUpdate(new WebSiteRequestDto
                {
                    WebSiteId = model.WebSiteId,
                    StatusCode = (int)response.Result.StatusCode,
                    StatusMessage = response.Result.StatusCode.ToString()
                });
            }            
        }
    }
}
