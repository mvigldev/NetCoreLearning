using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCoreLearning.Infrastructure.EventBus.NServiceBus;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NetCoreLearning.SchoolLessons.Worker
{
    /// <summary>
    /// Entry point of the app.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The service provider instance for the app to get a service on runtime.(Is needed only for the event handler of NService bus)
        /// </summary>
        public static IServiceProvider ServiceProviderInstance;

        /// <summary>
        /// Entry point of the app.
        /// </summary>
        public static async Task Main()
        {
            await CreateHostBuilder().Build().RunAsync();
        }

        /// <summary>
        /// Creates the host builder.
        /// </summary>
        /// <returns>The hots</returns>
        public static IHostBuilder CreateHostBuilder()
        {
            var configuration = new ConfigurationBuilder()// pre host build configuration
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .Build();

            return new HostBuilder()
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost.SetBasePath(Directory.GetCurrentDirectory());
                })
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.AddJsonFile("appsettings.json", optional: true);
                })
                .ConfigureServices(services =>
                {
                    // register ABS data context
                    services.Configure<SchoolLesonsAPISettings>(options => configuration.GetSection("SchoolLesonsAPISettings").Bind(options));
                    services.AddHttpClient();
                    services.AddSingleton<ISchoolLessonsService, SchoolLessonsService>();
                    services.AddHostedService<GradeAssignmentWorker>();
                    services.AddSingleton<IEventBus, EventBus>();
                    services.AddHostedService<EventBusService>();
                    services.AddStackExchangeRedisCache(options =>
                    {
                        options.Configuration = configuration.GetValue<string>("RedisConfiguration");
                        options.InstanceName = configuration.GetValue<string>("RedisInstanceName");
                    });
                    ServiceProviderInstance = services.BuildServiceProvider();
                });
        }
    }
}
