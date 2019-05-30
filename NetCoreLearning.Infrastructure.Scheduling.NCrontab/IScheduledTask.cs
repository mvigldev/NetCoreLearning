using System.Threading.Tasks;

namespace NetCoreLearning.Infrastructure.Scheduling.NCrontab
{
    /// <summary>
    /// Specifies a scheduled task that executed a task in scheduled time.
    /// </summary>
    public interface IScheduledTask
    {
        /// <summary>
        /// Gets the croon expression that the task will be executed.
        /// </summary>
        string Schedule { get; }

        /// <summary>
        /// Executes the asynchronous task according to the schedule.
        /// </summary>
        /// <returns>A <see cref="Task"/> that will execute the task.</returns>
        Task ExecuteAsync();
    }
}
