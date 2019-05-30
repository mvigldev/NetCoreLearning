using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NetCoreLearning.Infrastructure.EventBus.NServiceBus;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreLearning.Infrastructure.EventBus.Database
{
    /// <summary>
    /// THe service responsible for executing the event log database operations.
    /// </summary>
    /// <seealso cref="NetCoreLearning.Infrastructure.EventBus.Database.IEventLogService" />
    public class EventLogService : IEventLogService
    {
        private readonly EventLogContext _context;
        private readonly DbConnection _dbConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventLogService"/> class.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <exception cref="System.ArgumentNullException">dbConnection</exception>
        public EventLogService(DbConnection dbConnection)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            _context = new EventLogContext(
                new DbContextOptionsBuilder<EventLogContext>()
                    .UseSqlServer(_dbConnection)
                    .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
                    .Options);
        }

        /// <summary>
        /// Retrieves the event logs pending to publish asynchronous.
        /// </summary>
        /// <param name="eventModelType">Type of the event model.</param>
        /// <returns>A task that executes this operation</returns>
        public async Task<IEnumerable<EventLog>> RetrieveEventLogsPendingToPublishAsync(Type eventModelType)
        {
            return await _context.EventLogs
                .Where(e => e.State == EventStateEnum.NotPublished && e.EventTypeName == eventModelType.FullName)
                .OrderBy(o => o.CreationTime)
                .Select(e => e.DeserializeJsonContent(eventModelType))
                .ToListAsync();
        }

        /// <summary>
        /// Saves the event asynchronous in the event logging database.
        /// </summary>
        /// <typeparam name="T">The type of the event model</typeparam>
        /// <param name="event">The event to save.</param>
        /// <param name="transaction">The transaction to execute the operation.</param>
        /// <returns>
        /// A task that executes this operation
        /// </returns>
        /// <exception cref="System.ArgumentNullException">transaction - A {typeof(DbTransaction).FullName}</exception>
        public Task SaveEventAsync<T>(Event<T> @event, DbTransaction transaction) where T : class
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction), $"A {typeof(DbTransaction).FullName} is required as a pre-requisite to save the event.");
            }

            var eventLog = new EventLog(@event.Id, @event.CreationDate, @event.Model.GetType().FullName, @event.Model);

            _context.Database.UseTransaction(transaction);
            _context.EventLogs.Add(eventLog);

            return _context.SaveChangesAsync();
        }

        /// <summary>
        /// Marks the event as published asynchronous.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <returns>
        /// A task that executes this operation
        /// </returns>
        public Task MarkEventAsPublishedAsync(Guid eventId)
        {
            return UpdateEventStatus(eventId, EventStateEnum.Published);
        }

        /// <summary>
        /// Marks the event as in progress asynchronous.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <returns>
        /// A task that executes this operation
        /// </returns>
        public Task MarkEventAsInProgressAsync(Guid eventId)
        {
            return UpdateEventStatus(eventId, EventStateEnum.InProgress);
        }

        /// <summary>
        /// Marks the event as failed asynchronous.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <returns>
        /// A task that executes this operation
        /// </returns>
        public Task MarkEventAsFailedAsync(Guid eventId)
        {
            return UpdateEventStatus(eventId, EventStateEnum.PublishedFailed);
        }

        private Task UpdateEventStatus(Guid eventId, EventStateEnum status)
        {
            var eventLogEntry = _context.EventLogs.Find(eventId);
            eventLogEntry.State = status;

            if (status == EventStateEnum.InProgress) eventLogEntry.TimesSent++;

            _context.EventLogs.Update(eventLogEntry);

            return _context.SaveChangesAsync();
        }
    }
}
