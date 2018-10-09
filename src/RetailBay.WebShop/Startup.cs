using System;
using System.Text;
using AgileObjects.AgileMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RetailBay.Core;
using RetailBay.Core.Entities.Identity;
using RetailBay.Core.Entities.SystemDb;
using RetailBay.Infrastructure;
using RetailBay.Infrastructure.EntityFramework;
using RetailBay.Infrastructure.Multitenancy;
using StackExchange.Profiling.Storage;

namespace RetailBay.WebShop
{
    public class Startup
    {
        private IServiceCollection _services;
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Mapper.WhenMapping.UseConfigurations.From<Infrastructure.Mapper.MappingConfiguration>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<TenantDBContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromHours(1);
                options.Cookie.Name = "RetailBay.WebShop.Identity";
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.IsEssential = true;
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
            });

            services.AddCoreDependencies();
            services.AddInfrastructureDependencies();
            services.AddInfrastructureEFDependencies();
            services.AddMultitenancy<Tenant, TenantResolver>();

            services.AddMiniProfiler(options =>
            {
                // All of this is optional. You can simply call .AddMiniProfiler() for all defaults
                (options.Storage as MemoryCacheStorage).CacheDuration = TimeSpan.FromMinutes(60);
                options.SqlFormatter = new StackExchange.Profiling.SqlFormatters.InlineFormatter();
                options.TrackConnectionOpenClose = true;
            })
            .AddEntityFramework();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            _services = services;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                ListAllRegisteredServices(app);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMultitenancy<Tenant>();
            app.UseAuthentication();
            app.UseMiniProfiler();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void ListAllRegisteredServices(IApplicationBuilder app)
        {
            app.Map("/allservices", builder => builder.Run(async context =>
            {
                var sb = new StringBuilder();
                sb.Append("<h1>All Services</h1>");
                sb.Append("<table><thead>");
                sb.Append("<tr><th>Type</th><th>Lifetime</th><th>Instance</th></tr>");
                sb.Append("</thead><tbody>");
                foreach (var svc in _services)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td>{svc.ServiceType.FullName}</td>");
                    sb.Append($"<td>{svc.Lifetime}</td>");
                    sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</tbody></table>");
                await context.Response.WriteAsync(sb.ToString());
            }));
        }
    }
}
