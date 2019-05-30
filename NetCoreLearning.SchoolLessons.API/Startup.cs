using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetCoreLearning.Infrastructure.EventBus.Database;
using NetCoreLearning.Infrastructure.EventBus.Database.ScheduledTasks;
using NetCoreLearning.Infrastructure.EventBus.Database.Settings;
using NetCoreLearning.Infrastructure.EventBus.NServiceBus;
using NetCoreLearning.Infrastructure.Scheduling.NCrontab;
using NetCoreLearning.SchoolLessons.API.Database;
using NetCoreLearning.SchoolLessons.DomainModel;
using System.Linq;

namespace NetCoreLearning.SchoolLessons.API
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        IEventBus _eventBus;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration of the app.
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<PublishPendingEventsTaskSettings>(options => Configuration.GetSection("PublishpendingEventsTaskSettings").Bind(options));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMemoryCache();
            services.AddSwaggerForAPI()
            .AddJWTAuthentication(Configuration)
            .AddLogging(c => c.AddDebug())
            .AddSchoolLessonsDBContext(Configuration)
            .AddEventBusLoggingDBContext(Configuration)
            .AddSingleton<IHostedService, SchedulerWorker>()
            .AddTransient<IScheduledTask, PublishPendingEventsTask<GradeAssignment>>()
            .AddSingleton<IEventBus, EventBus>()
            .AddTransient<IEventLogServiceFactory, EventLogServiceFactory>()
            .AddTransient<IResilientTransaction, ResilientTransaction>()
            .AddTransient<IEventBusPublisher<GradeAssignment>, EventBusPublisher<GradeAssignment>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="applicationLifetime">The application lifetime.</param>
        /// <param name="env">The env.</param>
        /// <param name="context">The context.</param>
        /// <param name="cache">The cache.</param>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="logger">The logger.</param>
        public void Configure(
            IApplicationBuilder app,
            Microsoft.Extensions.Hosting.IApplicationLifetime applicationLifetime,
            Microsoft.Extensions.Hosting.IHostingEnvironment env,
            SchoolLessonsContext context,
            IMemoryCache cache,
            IEventBus eventBus,
            ILogger<ErrorDetails> logger)
        {
            _eventBus = eventBus;
            applicationLifetime.ApplicationStopping.Register(OnShutdown);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.ConfigureExceptionHandler(logger);
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test SchoolLessons API");
            });
            app.UseHttpsRedirection();
            app.UseMvc();
            InitCache(context, cache);
            StartEventBus();
        }

        private void StartEventBus()
        {
            _eventBus.Start(nameof(GradeAssignment), true);
        }

        /// <summary>
        /// Called when app stops.
        /// </summary>
        private void OnShutdown()
        {
            _eventBus.Stop();
        }

        /// <summary>
        /// Initializes the cache first time only.
        /// </summary>
        /// <param name="context">The context to access db.</param>
        /// <param name="cache">The cache to init.</param>
        private void InitCache(SchoolLessonsContext context, IMemoryCache cache)
        {
            cache.GetOrCreate("Students", entry =>
            {
                return context.Students.ToDictionary(c => c.Id, c => c);
            });

            cache.GetOrCreate("Lessons", entry =>
            {
                return context.Lessons.ToDictionary(c => c.Id, c => c);
            });

            cache.GetOrCreate("Professors", entry =>
            {
                return context.Professors.ToDictionary(c => c.Id, c => c);
            });
        }
    }
}
