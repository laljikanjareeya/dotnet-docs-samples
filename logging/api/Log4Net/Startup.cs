using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Log4NetSample
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();

            app.Map("", HelloWorldHandler);
        }

        private static void HelloWorldHandler(IApplicationBuilder app)
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            string response = "";
            // Check to ensure that projectId has been changed from placeholder value.
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead("log4net.config"));
            var element = log4netConfig.GetElementsByTagName("projectId").Item(0);
            string projectId = element.Attributes["value"].Value;
            if (projectId != "YOUR-PROJECT-ID")
            {
                // [START log4net_log_entry]
                // Retrieve a logger for this context.
                ILog log = LogManager.GetLogger(typeof(Startup));

                // Log some information to Google Stackdriver Logging.
                log.Info("Hello World.");
                // [END log4net_log_entry]

                // Pause for 5 seconds to ensure log entry is completed
                Thread.Sleep(5000);
                response = $"Log Entry created in project: {projectId}";
            }
            else
            {
                response = "Update log4net.config and replace YOUR-PROJECT" +
                "-ID with your project id, and recompile.";
            }

            app.Run(async context =>
            {
                await context.Response.WriteAsync(response);
            });
        }
    }
}
