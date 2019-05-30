using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetCoreLearning.SchoolLessons.DomainModel;
using NetCoreLearning.SchoolLessons.Razor.SignalR.Hubs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace NetCoreLearning.SchoolLessons.Razor.SignalR.HostedServices
{
    /// <summary>
    /// BackgroundService that reads distributed cache and sends added Grade Assignments to the client through hub
    /// </summary>
    /// <seealso cref="Microsoft.Extensions.Hosting.BackgroundService" />
    public class HubWorker : BackgroundService
    {
        private readonly ILogger<HubWorker> _logger;
        private readonly IHubContext<GradeAssignmentsHub, IShowLiveGradeAssignments> _gradeAssignmentsHub;
        private readonly IDistributedCache _cache;
        private int _previousGradeAssignmentsCount = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="HubWorker"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="gradeAssignmentsHub">The grade assignments hub.</param>
        /// <param name="cache">The cache.</param>
        /// <exception cref="System.ArgumentNullException">
        /// logger
        /// or
        /// gradeAssignmentsHub
        /// or
        /// cache
        /// </exception>
        public HubWorker(
            ILogger<HubWorker> logger,
            IHubContext<GradeAssignmentsHub,
                IShowLiveGradeAssignments> gradeAssignmentsHub,
            IDistributedCache cache)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _gradeAssignmentsHub = gradeAssignmentsHub ?? throw new ArgumentNullException(nameof(gradeAssignmentsHub));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        /// <summary>
        /// This method is called when the <see cref="T:Microsoft.Extensions.Hosting.IHostedService" /> starts. The implementation should return a task that represents
        /// the lifetime of the long running operation(s) being performed.
        /// </summary>
        /// <param name="stoppingToken">Triggered when <see cref="M:Microsoft.Extensions.Hosting.IHostedService.StopAsync(System.Threading.CancellationToken)" /> is called.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> that represents the long running operations.
        /// </returns>
        /// <remarks>
        /// Every 1 minute reads cache and sends delta items to all clients.
        /// </remarks>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Getting Live Grade Assignments at: {DateTime.Now}");
                var liveGradeAssignments = await GetCachedGradeAssignments();
                int currentGradeAssignmentsCount = liveGradeAssignments.Count();

                if (_previousGradeAssignmentsCount != currentGradeAssignmentsCount)
                {
                    if (currentGradeAssignmentsCount > _previousGradeAssignmentsCount)// in case new grade assignments, send only the new
                    {
                        _logger.LogInformation($"Sending new Live Grade Assignments at: {DateTime.Now}");
                        int itemsToSkip = currentGradeAssignmentsCount - _previousGradeAssignmentsCount;
                        int itemsToTake = currentGradeAssignmentsCount - _previousGradeAssignmentsCount;
                        await _gradeAssignmentsHub.Clients.All.ShowLiveGradeAssignments(
                            liveGradeAssignments.Select(c => c.Value)
                            .Skip(_previousGradeAssignmentsCount)
                            .Take(itemsToTake)
                            .ToList());
                    }
                    else// reset case
                    {
                        _logger.LogInformation($"Resetting Live Grade Assignments at: {DateTime.Now}");
                        await _gradeAssignmentsHub.Clients.All.ResetLiveGradeAssignments();
                    }
                }

                _previousGradeAssignmentsCount = currentGradeAssignmentsCount;

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private async Task<Dictionary<Guid, GradeAssignment>> GetCachedGradeAssignments()
        {
            string cachedGradeAssignments = await _cache.GetStringAsync("GradeAssignments");
            if (cachedGradeAssignments == null)
            {
                return new Dictionary<Guid, GradeAssignment>();
            }
            return JsonConvert.DeserializeObject<Dictionary<Guid, GradeAssignment>>(cachedGradeAssignments);
        }

    }
}
