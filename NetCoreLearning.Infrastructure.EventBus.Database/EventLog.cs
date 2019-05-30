using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreLearning.Infrastructure.EventBus.Database
{
    /// <summary>
    /// Class that represent a log entry for an event of the service bus.
    /// </summary>
    public class EventLog
    {
        /// <summary>
        /// Gets the event identifier.
        /// </summary>
        [Required]
        [Key]
        public Guid EventId { get; private set; }

        /// <summary>
        /// Gets the event type name.
        /// </summary>
        [Required]
        [MaxLength(128)]
        public string EventTypeName { get; private set; }

        /// <summary>
        /// Gets the state of the event.
        /// </summary>
        [Required]
        public EventStateEnum State { get; set; }

        /// <summary>
        /// Gets the event creation date.
        /// </summary>
        [Required]
        public DateTime CreationTime { get; private set; }

        /// <summary>
        /// Gets the number of tries that the event was tried to be published.
        /// </summary>
        [Required]
        public int TimesSent { get; set; }

        /// <summary>
        /// Gets the model of the event in Json format.
        /// </summary>
        [Required]
        public string Content { get; private set; }

        /// <summary>
        /// Gets the model of the event.
        /// </summary>
        [NotMapped]
        public Object EventModel { get; private set; }

        private EventLog() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventLog"/> class.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="eventCreationDate">The event creation date.</param>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="eventModel">The event model.</param>
        public EventLog(Guid eventId, DateTime eventCreationDate, string eventType, object @eventModel)
        {
            EventId = eventId;
            CreationTime = eventCreationDate;
            EventTypeName = eventType;
            Content = JsonConvert.SerializeObject(@eventModel);
            State = EventStateEnum.NotPublished;
        }

        /// <summary>
        /// De serializes the content of the event from json to object of type <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type of the model of the event.</param>
        /// <returns>this instance</returns>
        public EventLog DeserializeJsonContent(Type type)
        {
            EventModel = JsonConvert.DeserializeObject(Content, type);
            return this;
        }
    }
}