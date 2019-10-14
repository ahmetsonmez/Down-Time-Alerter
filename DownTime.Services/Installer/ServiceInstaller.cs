using DownTime.Core.Hash;
using DownTime.Data.Context;
using DownTime.Services.Concrete;
using DownTime.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DownTime.Services.Installer
{
    public class ServiceInstaller
    {
        public IConfiguration Configuration { get; }

        public ServiceInstaller(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Services initialized here because we initialized db context here.
        /// I didn't want access data layer in web layer. Only services layer must be accessed to the data layer.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {          
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("appConnection")));

            services.AddScoped<DbContext, AppDbContext>();
            //Services Injection            
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWebSiteService, WebSiteService>();
            services.AddScoped<IHangfireBootstrapper, HangfireBootstrapper>();
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IWebSiteRequestService, WebSiteRequestService>();
            services.AddScoped<ISaltHash, Sha512Hash>();
        }
    }
}
