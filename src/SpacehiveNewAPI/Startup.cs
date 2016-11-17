using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Spacehive.NewDataAccess.Interfaces;
using Spacehive.NewDataAccess.Concrete;
using Spacehive.NewServices.Interfaces;
using Spacehive.NewServices.Concrete;
using Spacehive.NewCrossCutting;

namespace SpacehiveNewAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            // Setup options with DI
            services.AddOptions();

            // Configure MySubOptions using a sub-section of the appsettings.json file
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            #region Mongodb DI

            services.AddScoped<IMongoDbRepository, MongoDbRepository>();
            services.AddScoped<INewSurveyService, NewSurveyService>();

            #endregion


            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
