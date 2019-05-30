using NetCoreLearning.Infrastructure.EventBus.NServiceBus;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace NetCoreLearning.Infrastructure.EventBus.Database
{
    /// <summary>
    /// Contract for event log service actions
    /// </summary>
    public interface IEventLogService
    {
        /// <summary>
        /// Saves the event asynchronous in the event logging database.
        /// </summary>
        /// <typeparam name="T">The type of the event model</typeparam>
        /// <param name="event">The event to save.</param>
        /// <param name="transaction">The transaction to execute the operation.</param>
        /// <returns>A task that executes this operation</returns>
        Task SaveEventAsync<T>(Event<T> @event, DbTransaction transaction) where T : class;

        /// <summary>
        /// Marks the event as published asynchronous.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <returns>A task that executes this operation</returns>
        Task MarkEventAsPublishedAsync(Guid eventId);

        /// <summary>
        /// Marks the event as in progress asynchronous.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <returns>A task that executes this operation</returns>
        Task MarkEventAsInProgressAsync(Guid eventId);

        /// <summary>
        /// Marks the event as failed asynchronous.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <returns>A task that executes this operation</returns>
        Task MarkEventAsFailedAsync(Guid eventId);

        /// <summary>
        /// Retrieves the event logs pending to be publish asynchronous.
        /// </summary>
        /// <param name="eventModelType">Type of the event model.</param>
        /// <returns>A task that executes this operation</returns>
        Task<IEnumerable<EventLog>> RetrieveEventLogsPendingToPublishAsync(Type eventModelType);

    }
}