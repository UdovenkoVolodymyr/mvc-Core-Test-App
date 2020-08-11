using Microsoft.AspNetCore.Identity;
using MvcEducationApp.Domain.Core.Models;
using MvcEducationApp.Infrastructure.Data;
using MvcEducationApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcEducationApp
{
    public static class InitDB
    {
        public async static Task Initialize(MvcEducationDBContext context, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            var adminEmail = "email@admin.com";
            var adminPass = "adminAdmin1!";
            var defaultUserEmail = "email@user.com";
            var defaultUserPass = "userUser1!";

            var admin = new User { Email = adminEmail, UserName = adminEmail, Year = 1776 };
            var defaultUser = new User { Email = defaultUserEmail, UserName = defaultUserEmail, Year = 988 };

            var Roles = roleManager.Roles.Select(x => x.Name).ToList();
            if (!Roles.Any(s => "admin".Contains(s)))
            {
                await roleManager.CreateAsync( new IdentityRole { Name = "admin" } );   
                await userManager.CreateAsync(admin, adminPass);
                await userManager.AddToRoleAsync(admin, "admin");
            }

            var defaultUserCheck = await userManager.FindByNameAsync("DefaultUser");
            if (defaultUserCheck == null)
            {
                await userManager.CreateAsync(defaultUser, defaultUserPass);
            }
            context.SaveChanges();

            if (!context.Courses.Any())
            {
                var adminUser = await userManager.FindByNameAsync(adminEmail);
                context.Courses.AddRange(
                    new Course
                    {
                        Title = "ASP.NET Core MVC",
                        Сategory = "programming",
                        Price = 600,
                        LastUpdated = DateTime.UtcNow,
                        User = adminUser
                    },
                     new Course
                     {
                         Title = "Python",
                         Сategory = "programming",
                         Price = 600,
                         LastUpdated = DateTime.UtcNow,
                         User = adminUser
                     }
                );
                context.SaveChanges();
            }
        }
    }
}
