using System;
using System.Linq;
using DownTime.Core.Hash;
using DownTime.Data.Context;
using DownTime.Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DownTime.Web.Models
{
    public static class SeedData
    {
        /// <summary>
        /// Generates db tables and inserts some dummy data.
        /// </summary>
        /// <param name="app"></param>
        public static void Seed(IApplicationBuilder app)
        {
            AppDbContext context = app.ApplicationServices.GetRequiredService<AppDbContext>();
            context.Database.Migrate();

            if (!context.Users.Any())
            {
                var hash = new Sha512Hash();
                var salt = hash.GetSalt();
                var userHash = hash.GetHash("ahmet.sonmez37@gmail.com" + "password", salt);

                context.Users.Add(new User()
                {
                    IsActive = true,
                    Salt = salt,
                    Hash = userHash,
                    Created = DateTime.Now,
                    Email = "ahmet.sonmez37@gmail.com",
                    FirstName = "Ahmet",
                    LastName = "Sönmez",
                    UserName = "admin",
                });

                context.SaveChanges();
            }
        }
    }
}
