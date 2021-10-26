using APVN.LeadsPlatform.Config;
using APVN.LeadsPlatform.Logging;
using APVN.LeadsPlatform.Logging.Extensions;
using APVN.LeadsPlatform.Logging.NLogCustom;
using APVN.LeadsPlatform.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace APVN.LeadsPlatform
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", true, true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
               .AddEnvironmentVariables();
            Configuration = builder.Build();
            environment = env;
            AppSettings.Instance.SetConfiguration(configuration);


            //var configFilePath = configuration.GetSection("Logging:Providers:NLog:ConfigFilePath").Get<string>();
            //NLogLoggingBuilder.UseNLog(configFilePath, configuration.GetSection("Logging:KafkaTaget").Get<string>(), LogSourceTypeEnums.LeadsPlatform);
            System.Console.WriteLine("Hello Leads---------------->" + DateTime.Now);
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment environment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IoC.RegisterTypes(services);
            if (!environment.IsDevelopment())
            {
                services.AddSpaStaticFiles(configuration =>
                {
                    configuration.RootPath = "wwwroot/ClientApp/dist";
                });
            }
            services.AddControllersWithViews();
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
                endpoints.MapControllers();
                //endpoints.MapDefaultControllerRoute();
            });
            if (!environment.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseSpa(spa =>
            {
                // spa.Options.SourcePath = "../ClientApp";
                //spa.UseAngularCliServer("start");
                if (environment.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://127.0.0.1:4500");
                }
            });
        }
    }
}