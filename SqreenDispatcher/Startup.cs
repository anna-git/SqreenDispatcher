using GeekLearning.Domain;
using GeekLearning.Email;
using GeekLearning.Storage;
using GeekLearning.Storage.Configuration;
using GeekLearning.Templating;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SqreenDispatcher.Services;
using SqreenDispatcher.Services.Targets;
using System.Collections.Generic;
using System.Linq;

namespace SqreenDispatcher
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
           .SetBasePath(env.ContentRootPath)
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
            _contentRootPath = env.ContentRootPath;
        }

        private string _contentRootPath;

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

            var targetsConfig = Configuration.GetSection("Targets").Get<List<string>>()?? Enumerable.Empty<string>();
            foreach (var item in targetsConfig)
            {
                var type = TargetsConsts.targetTypes[item];
                services.AddScoped(typeof(ITarget), type);
            }

            services.Configure<StorageOptions>(Configuration.GetSection("Storage"));
            var connString = Configuration.GetConnectionString("DefaultConnection");
            services.AddStorage(Configuration).AddFileSystemStorage(_contentRootPath);
            services.AddTemplating().AddHandlebars();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            services.AddEmail().AddSmtpEmail();
            services.Configure<EmailOptions>(Configuration.GetSection("Email"));
            services.AddMemoryCache();

            var options = Configuration.GetSection("Sqreen").Get<SqreenOptions>();
            services.AddScoped(_ => options);
            services.AddScoped(_ => new DbOptions(connString));
            services.AddScoped(sp => new Dispatcher(sp.GetServices<ITarget>()));
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
