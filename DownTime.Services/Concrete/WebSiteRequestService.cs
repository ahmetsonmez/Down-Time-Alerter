using DownTime.CommonModel.Models;
using DownTime.Data.Context;
using DownTime.Data.Entities;
using DownTime.Data.Repositories;
using DownTime.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DownTime.Services.Concrete
{
    public class WebSiteRequestService : IWebSiteRequestService
    {
        private readonly BaseRepository<WebSiteRequest> _repository;
        public WebSiteRequestService(DbContextOptions<AppDbContext> options)
        {
            _repository = new Repository<WebSiteRequest>(options);
        }

        /// <summary>
        /// Returns the list of website requests according to the id value.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<WebSiteRequestDto> GetWebSiteRequests(int? id)
        {
            IQueryable<WebSiteRequest> model;
            if (id.HasValue)
                model = _repository.List().Result.Data.Include(y => y.WebSite).Where(z => z.WebSiteId == id);
            else
                model = _repository.List().Result.Data.Include(y => y.WebSite);
            
            if (model == null || !model.Any()) return new List<WebSiteRequestDto>();

            var returnList = new List<WebSiteRequestDto>();
            foreach (var webSite in model)
            {
                returnList.Add(new WebSiteRequestDto
                {
                    Id = webSite.Id,
                    StatusCode = webSite.StatusCode,
                    StatusMessage = webSite.StatusMessage,
                    WebSiteId = webSite.WebSiteId,
                    Created = webSite.Created,
                    WebSite = new WebSiteDto
                    {
                        Id = webSite.WebSiteId,
                        Name = webSite.WebSite.Name,
                        Url = webSite.WebSite.Url,
                        IsActive = webSite.WebSite.IsActive,
                        UserId = webSite.WebSite.UserId,
                        RequestInterval = webSite.WebSite.RequestInterval
                    }
                });
            }
            return returnList;
        }

        /// <summary>
        /// Creates or Updates the list of website requests
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CreateOrUpdate(WebSiteRequestDto model)
        {
            var webSiteModel = new WebSiteRequest()
            {               
                StatusCode = model.StatusCode,
                StatusMessage = model.StatusMessage,
                WebSiteId = model.WebSiteId
            };
            return model.Id > 0
                ? _repository.Update(webSiteModel).Result.Data
                : _repository.Create(webSiteModel).Result.Data;
        }
    }
}
