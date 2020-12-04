using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SqreenDispatcher.Services;
using SqreenDispatcher.Services.Targets;
using SqreenDispatcher.WebHooks;
using System.Collections.Generic;
using System.Linq;

namespace SqreenDispatcher
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddLogging();
            services.AddWebhooks(opt =>
            {
                // default is "webhooks"
                opt.RoutePrefix = "wh";
            });
            var targetsConfig = Configuration.GetValue<string[]>("Targets");
            if (targetsConfig.Any(t => t == "email"))
            {
                services.AddTransient<ITarget, EmailTarget>();
            }
            if (targetsConfig.Any(t => t == "log"))
            {
                services.AddTransient<ITarget, LogTarget>();
            }
            if (targetsConfig.Any(t => t == "database"))
            {
                services.AddTransient<ITarget, DatabaseTarget>();
            }

            services.AddTransient(sp => new Dispatcher(sp.GetServices<ITarget>()));
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
