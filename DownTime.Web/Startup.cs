using AutoMapper;
using DownTime.Services.Installer;
using DownTime.Services.Interface;
using DownTime.Services.Logger;
using DownTime.Web.Models;
using Hangfire;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DownTime.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            services.AddAuthentication
                    (CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            services.AddHangfire(x =>
                x.UseSqlServerStorage(
                    Configuration.GetConnectionString("appConnection")));

            var serviceInstaller = new ServiceInstaller(Configuration);
            serviceInstaller.ConfigureServices(services);          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddProvider(new FileLogProvider());

            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/{0}");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            //inject hangfire
            app.UseHangfireServer();
            app.UseHangfireDashboard("/hangfire");

            RecurringJob.AddOrUpdate<IHangfireBootstrapper>(
                hangfireStart => hangfireStart.SetJobsWebSites(), Cron.Minutely);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //Add seeddata
            SeedData.Seed(app);
        }
    }
}
