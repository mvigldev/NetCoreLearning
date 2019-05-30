using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NCrontab;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreLearning.Infrastructure.Scheduling.NCrontab
{
    /// <summary>
    /// This is a background service that executes scheduled tasks of type <see cref="IScheduledTask"/><br></br>
    /// that have been added as services in Service provider utilizing NCrontab nuget package.
    /// </summary>
    /// <seealso cref="Microsoft.Extensions.Hosting.BackgroundService" />
    public class SchedulerWorker : BackgroundService
    {
        #region Private fields
        private bool _erroroccurred;
        private readonly ILogger<SchedulerWorker> _logger;
        private readonly ConcurrentDictionary<int, Task> _tasks = new ConcurrentDictionary<int, Task>();
        private readonly List<SchedulerTaskWrapper> _scheduledTasks = new List<SchedulerTaskWrapper>();
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SchedulerWorker"/> class.
        /// </summary>
        /// <param name="logger">The logger interface.</param>
        /// <param name="scheduledTasks">The scheduled tasks to initiate.</param>
        /// <exception cref="ArgumentNullException">
        /// logger
        /// or
        /// scheduledTasks
        /// </exception>
        /// <remarks>
        /// This class runs <see cref="IScheduledTask"/>s as new <see cref="Task"/>s asynchronously in the default task scheduler.<br></br>
        /// It keeps the tasks in a Concurrent Dictionary so that it waits all the running tasks to be finished in case of gracefully shut down.<br></br>
        /// In case of an error to one of any scheduled tasks, it stops gracefully because no error is expected.
        /// </remarks>
        public SchedulerWorker(ILogger<SchedulerWorker> logger, IEnumerable<IScheduledTask> scheduledTasks)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            if (scheduledTasks == null) throw new ArgumentNullException(nameof(scheduledTasks));

            var referenceTime = DateTime.UtcNow;
            foreach (var scheduledTask in scheduledTasks)
            {
                _scheduledTasks.Add(new SchedulerTaskWrapper
                {
                    Schedule = CrontabSchedule.Parse(scheduledTask.Schedule),
                    Task = scheduledTask,
                    NextRunTime = referenceTime
                });
            }
        }

        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
        /// <returns>A <see cref="Task"/> that starts this instance</returns>
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Worker started at: {DateTime.Now}");

            return base.StartAsync(cancellationToken);
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
        /// <returns>A <see cref="Task"/> that stops gracefully this instance</returns>
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Worker stopping at: {DateTime.Now}");

            Task t = base.StopAsync(cancellationToken);

            RemoveNotRunningTasks();
            WaitRunningTasksToFinish();
            _logger.LogInformation($"Worker stopped at: {DateTime.Now}");

            return t;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public override void Dispose()
        {
            _logger.LogInformation($"Worker disposed at: {DateTime.Now}");
            base.Dispose();
        }

        /// <summary>
        /// This method is called when the <see cref="T:Microsoft.Extensions.Hosting.IHostedService" /> starts.<br></br>
        /// Runs forever until a cancellation is requested and executes the scheduled tasks.
        /// </summary>
        /// <param name="stoppingToken">Triggered when <see cref="M:Microsoft.Extensions.Hosting.IHostedService.StopAsync(System.Threading.CancellationToken)" /> is called.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> that represents the long running operations.
        /// </returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Worker Initialized at: {DateTime.Now}");

            while (!stoppingToken.IsCancellationRequested)
            {
                if (_erroroccurred)
                {
                    _logger.LogError("Worker exit.........");
                    break;
                }

                _logger.LogDebug($"Looping");
                try
                {
                    var referenceTime = DateTime.UtcNow;
                    var tasksThatShouldRun = _scheduledTasks.Where(t => t.ShouldRun(referenceTime)).ToList();

                    foreach (var taskThatShouldRun in tasksThatShouldRun)
                    {
                        taskThatShouldRun.Increment();
                        var task = Task.Run(async () => { await taskThatShouldRun.Task.ExecuteAsync(); }).ContinueWith(ct => { ContinuationTask(ct); });
                        _logger.LogDebug("Adding task : {id}", task.Id);
                        _tasks.TryAdd(task.Id, task);
                        if (_erroroccurred)
                        {
                            _logger.LogWarning("Worker Stopping init any more tasks.........");
                            break;
                        }
                    }
                }
                catch (TaskCanceledException)
                {
                    _logger.LogError("Worker TaskCanceledException requested at: {time}", DateTimeOffset.Now);
                }

                RemoveNotRunningTasks();

                if (_erroroccurred)
                {
                    _logger.LogWarning("Worker exit.........");
                    break;
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);

            }
            _logger.LogInformation($"Worker exit loop at: {DateTime.Now}");
        }

        #region Pivate Methods
        private void RemoveNotRunningTasks()
        {
            var notRunningTasks = _tasks.Select(c => c.Value).Where(v => v.Status == TaskStatus.RanToCompletion || v.Status == TaskStatus.Faulted || v.Status == TaskStatus.Canceled);
            foreach (var notRunningTask in notRunningTasks)
            {
                _logger.LogDebug("Removing task : {id}", notRunningTask.Id);
                _tasks.Remove(notRunningTask.Id, out _);
            }
        }

        private void ContinuationTask(Task t)
        {
            if (t.Exception != null)
            {
                _logger.LogError("Worker================== Exception occurred at: {time}==========================", DateTimeOffset.Now);
                _logger.LogError(t.Exception.ToString(), DateTimeOffset.Now);
                _erroroccurred = true;
            }
        }

        /// <summary>
        /// Waits the running tasks to finish.
        /// </summary>
        private void WaitRunningTasksToFinish()
        {
            var tasksToWait = _tasks.Select(c => c.Value).ToArray();
            if (tasksToWait.Any())
            {
                _logger.LogInformation($"Waiting {tasksToWait.Count().ToString()} tasks to finish. Ids:{String.Join(',', tasksToWait.Select(c => c.Id))}");
                Task.WaitAll(tasksToWait.ToArray());
            }
            else
            {
                _logger.LogInformation($"No tasks to wait");
            }
        }
        #endregion
    }
}
