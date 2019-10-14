using System.Linq;
using DownTime.CommonModel.Models;
using DownTime.Data.Context;
using DownTime.Data.Entities;
using DownTime.Data.Repositories;
using DownTime.Services.Interface;
using Hangfire;
using Microsoft.EntityFrameworkCore;

namespace DownTime.Services.Concrete
{
    public class HangfireBootstrapper : IHangfireBootstrapper
    {
        private readonly BaseRepository<WebSite> _repository;
        private readonly IRequestService _requestService;
        public HangfireBootstrapper(DbContextOptions<AppDbContext> options, IRequestService requestService)
        {
            _requestService = requestService;
            _repository = new Repository<WebSite>(options);
        }

        /// <summary>
        /// Sets jobs for web sites
        /// </summary>
        public void SetJobsWebSites()
        {
            //TODO : Cache mechanism could be written but time was limited.
            var webSites = _repository.List().Result.Data.Include(y => y.User);
            var list = webSites.Where(x => x.IsActive).ToList();

            foreach (var webSite in list)
            {
                RecurringJob.AddOrUpdate("Website" + webSite.Name,
                    () => _requestService.Request(new RequestModel
                        {Url = webSite.Url, Email = webSite.User.Email, SiteName = webSite.Name,WebSiteId = webSite.Id}),
                    Cron.MinuteInterval(webSite.RequestInterval));
            }
        }
    }
}
