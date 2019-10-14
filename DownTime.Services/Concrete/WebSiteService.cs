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
    public class WebSiteService : IWebSiteService
    {
        private readonly BaseRepository<WebSite> _repository;
        public WebSiteService(DbContextOptions<AppDbContext> options)
        {
            _repository = new Repository<WebSite>(options);
        }

        /// <summary>
        /// Returns the model of website according to the id value.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WebSiteDto Get(int id)
        {
            var model = _repository.Get(y => y.Id == id).Result.Data ?? null;
            if (model != null)
            {
                return new WebSiteDto
                {
                    IsActive = model.IsActive,
                    Id = model.Id,
                    Name = model.Name,
                    UserId = model.UserId,
                    RequestInterval = model.RequestInterval,
                    Url = model.Url
                };
            }
            return new WebSiteDto();
        }

        /// <summary>
        /// Returns the list of websites.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WebSiteListDto> GetWebSites()
        {            
            var model =_repository.List().Result.Data.Include(y=>y.User);                          
            if (model == null || !model.Any()) return new List<WebSiteListDto>();

            return model.Select(webSite => new WebSiteListDto
                {
                    Name = webSite.Name,
                    Url = webSite.Url,
                    IsActive = webSite.IsActive,
                    Id = webSite.Id,
                    RequestInterval = webSite.RequestInterval,
                    User = new UserDto
                    {
                        IsActive = webSite.User.IsActive,
                        Id = webSite.User.Id,
                        Email = webSite.User.Email,
                        FirstName = webSite.User.FirstName,
                        LastName = webSite.User.LastName,
                        UserName = webSite.User.UserName
                    }
                }).ToList();
        }

        /// <summary>
        /// Creates or Updates the list of website requests
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CreateOrUpdate(WebSiteDto model)
        {           
            var webSiteModel = new WebSite()
            {
                Id =  model.Id,
                IsActive = model.IsActive,
                Name = model.Name,
                RequestInterval = model.RequestInterval,
                Url = model.Url,
                UserId = model.UserId,
                IsSetJob = false
            };
            return model.Id > 0
                ? _repository.Update(webSiteModel).Result.Data
                : _repository.Create(webSiteModel).Result.Data;            
        }       
    }
}
