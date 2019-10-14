using System.Collections.Generic;
using DownTime.CommonModel.Models;

namespace DownTime.Services.Interface
{
    public interface IWebSiteRequestService
    {
        /// <summary>
        /// Returns the list of website requests according to the id value.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<WebSiteRequestDto> GetWebSiteRequests(int? id);

        /// <summary>
        /// Creates or Updates the list of website requests
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool CreateOrUpdate(WebSiteRequestDto model);        
    }
}
