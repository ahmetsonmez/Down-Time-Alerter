using System.Collections.Generic;
using DownTime.CommonModel.Models;

namespace DownTime.Services.Interface
{
    public interface IWebSiteService
    {
        /// <summary>
        /// Returns the model of website according to the id value.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        WebSiteDto Get(int id);
        /// <summary>
        /// Returns the list of websites.
        /// </summary>
        /// <returns></returns>
        IEnumerable<WebSiteListDto> GetWebSites();
        /// <summary>
        /// Creates or Updates the list of website requests
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool CreateOrUpdate(WebSiteDto model);        
    }
}
