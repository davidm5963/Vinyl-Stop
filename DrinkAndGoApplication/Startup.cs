using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using AlbumStore.Data.Interfaces;
using AlbumStore.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using AlbumStore.Data.Repositories;
using AlbumStore.Data.Models;
using Microsoft.AspNetCore.Identity;
using AlbumStore.ViewModels;
using AlbumStore.Services;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace AlbumStore
{
    public class Startup
    {
        public static IConfiguration Configuration;

        public Startup(IHostingEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                           .SetBasePath(hostingEnvironment.ContentRootPath)
                           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                           .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });

            services.AddTransient<IEmailSender, EmailSender>();

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IAlbumRepository, AlbumRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.Configure<EmailConfiguration>(options => Configuration.GetSection("EmailConfiguration").Bind(options));
            services.AddScoped(sp => ShoppingCart.GetCart(sp));

            services.AddSession();
            services.AddMemoryCache();
            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            app.UseSession();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseAuthentication();
            app.UseMvc
                (routes => 
                    {
                        routes.MapRoute(name: "CategoryFilter", template: "Album/{action}/{category?}", defaults: new { Controller = "Album", action = "List" });
                        routes.MapRoute(name: "Default", template: "{controller=Home}/{action=Index}/{Id?}");
                        routes.MapRoute(name: "AlbumList", template: "{controller=Album}/{action=List}");
                    }
                );

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

        }
    }
}
