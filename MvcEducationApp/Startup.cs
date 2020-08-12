using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MvcEducation.Domain.Interfaces;
using MvcEducationApp.Domain.Core.Models;
using MvcEducationApp.Infrastructure.Data;
using MvcEducationApp.Models;
using MvcEducationApp.Services;

namespace MvcEducationApp
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
            services.AddDbContextPool<MvcEducationDBContext>(options => options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("LocalDBConnection")));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<MvcEducationDBContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddControllersWithViews();
            services.AddMvc();

            services.AddTransient<IMessageSender, EmailMessageSender>();
            services.AddSingleton<DebugLogger>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{Id?}",
                    new { controller= "Home", action = "index" });
            });

            app.UseRequestInfo();
        }
    }
}
