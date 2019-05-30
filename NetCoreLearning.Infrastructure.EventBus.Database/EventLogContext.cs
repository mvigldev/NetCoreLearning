using Microsoft.EntityFrameworkCore;

namespace NetCoreLearning.Infrastructure.EventBus.Database
{
    /// <summary>
    /// The event log entries data access.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class EventLogContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventLogContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public EventLogContext(DbContextOptions<EventLogContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the event logs.
        /// </summary>
        public DbSet<EventLog> EventLogs { get; set; }

    }
}
