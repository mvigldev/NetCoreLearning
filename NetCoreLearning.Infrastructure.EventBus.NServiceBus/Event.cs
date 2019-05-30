using NServiceBus;
using System;

namespace NetCoreLearning.Infrastructure.EventBus.NServiceBus
{
    /// <summary>
    /// Represents an event hat will trigger a job passing model <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of event</typeparam>
    /// <seealso cref="IEvent" />
    public class Event<T> : IEvent where T : class
    {
        /// <summary>
        /// Gets or sets the model for the event.
        /// </summary>
        public T Model { get; set; }

        /// <summary>
        /// Gets or sets the unique id of the event.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the creation date of the event.
        /// </summary>
        public DateTime CreationDate { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Event{T}"/> class.
        /// </summary>
        public Event()
        {
            this.Id = Guid.NewGuid();
            this.CreationDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Event{T}" /> class.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="creationDate">The creation date of the event.</param>
        /// <param name="model">The event model.</param>
        public Event(Guid eventId, DateTime creationDate, T model)
        {
            this.Id = eventId;
            this.CreationDate = creationDate;
            this.Model = model;
        }
    }
}
