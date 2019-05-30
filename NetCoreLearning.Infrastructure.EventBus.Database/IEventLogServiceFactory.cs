using Microsoft.EntityFrameworkCore;

namespace NetCoreLearning.Infrastructure.EventBus.Database
{
    /// <summary>
    /// Contract for event log service factory
    /// </summary>
    public interface IEventLogServiceFactory
    {
        /// <summary>
        /// Creates an event log service.
        /// </summary>
        /// <param name="context">The context that will be used for data access.</param>
        /// <returns></returns>
        IEventLogService CreateEventLogService(DbContext context);

    }
}