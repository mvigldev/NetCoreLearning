using NetCoreLearning.Infrastructure.EventBus.Database.ScheduledTasks;

namespace NetCoreLearning.Infrastructure.EventBus.Database.Settings
{
    /// <summary>
    /// Settings from app.json for <see cref="PublishPendingEventsTask{T}"/>
    /// </summary>
    public class PublishPendingEventsTaskSettings
    {
        /// <summary>
        /// Gets or sets the cron-expression.
        /// </summary>
        public string Schedule { get; set; }
    }
}
