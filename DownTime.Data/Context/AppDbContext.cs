using DownTime.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DownTime.Data.Context
{
    public class AppDbContext : DbContext
    {

        /// <summary>
        /// Generates db tables.
        /// </summary>
        /// <param name="options"></param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<WebSiteRequest> WebSiteRequests { get; set; }
        public DbSet<WebSite> WebSites { get; set; }
    }
}
