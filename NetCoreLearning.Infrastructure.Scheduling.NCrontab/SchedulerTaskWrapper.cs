using NCrontab;
using System;

namespace NetCoreLearning.Infrastructure.Scheduling.NCrontab
{
    /// <summary>
    /// Wrapper class for NCrontab scheduler instance representation for a specific task.
    /// </summary>
    public class SchedulerTaskWrapper
    {
        /// <summary>
        /// Gets or sets the <see cref="CrontabSchedule"/>.
        /// </summary>
        public CrontabSchedule Schedule { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IScheduledTask"/> to execute.
        /// </summary>
        public IScheduledTask Task { get; set; }

        /// <summary>
        /// Gets or sets the last run time of the task.
        /// </summary>
        public DateTime LastRunTime { get; set; }

        /// <summary>
        /// Gets or sets the next run time of the task.
        /// </summary>
        public DateTime NextRunTime { get; set; }

        /// <summary>
        /// Increments this instance by one occurrence.
        /// </summary>
        public void Increment()
        {
            LastRunTime = NextRunTime;
            NextRunTime = Schedule.GetNextOccurrence(NextRunTime);
        }

        /// <summary>
        /// Indicates whether this scheduler should run.
        /// </summary>
        /// <param name="currentTime">The current time.</param>
        /// <returns><c>true</c> in case it should run;otherwise <c>false</c></returns>
        public bool ShouldRun(DateTime currentTime)
        {
            return NextRunTime < currentTime && LastRunTime != NextRunTime;
        }
    }
}
