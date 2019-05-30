using Microsoft.EntityFrameworkCore;

namespace NetCoreLearning.Infrastructure.EventBus.Database
{

    /// <summary>
    /// <see cref="IEventLogServiceFactory"/> factory implementation.
    /// </summary>
    /// <seealso cref="NetCoreLearning.Infrastructure.EventBus.Database.IEventLogServiceFactory" />
    public class EventLogServiceFactory : IEventLogServiceFactory
    {
        /// <summary>
        /// Creates an event log service.
        /// </summary>
        /// <param name="context">The context that will be used for data access.</param>
        /// <returns>An <see cref="IEventLogService"/> instance.</returns>
        public IEventLogService CreateEventLogService(DbContext context)
        {
            if (context == null) throw new System.ArgumentNullException(nameof(context));
            return new EventLogService(context.Database.GetDbConnection());
        }
    }
}