using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjektInzynier.Helpers;
using ProjektInzynier.Interfaces;
using ProjektInzynier.Models;

namespace ProjektInzynier
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<EFCContext>();
            services.AddHttpContextAccessor();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddScoped<CartModel, SessionCart>();
            var connectionString = Configuration["ConnectionStrings:Default"];
            services.AddDbContext<EFCContext>(opt => opt.UseSqlServer(connectionString),
                ServiceLifetime.Transient);
        }

        protected IConfigurationRoot Configuration;

        public Startup()
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddXmlFile("appsettings.xml");
            Configuration = configurationBuilder.Build();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            app.UseStaticFiles();
        }
    }
}
