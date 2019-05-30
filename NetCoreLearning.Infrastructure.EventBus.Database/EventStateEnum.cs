namespace NetCoreLearning.Infrastructure.EventBus.Database
{
    /// <summary>
    /// Represents an enumeration of the states that an event can take.
    /// </summary>
    public enum EventStateEnum : byte
    {
        /// <summary>
        /// During event creation.
        /// </summary>
        NotPublished = 0,


        /// <summary>
        /// Just before trying to bublish
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// Just after published
        /// </summary>
        Published = 2,

        /// <summary>
        /// Just after trying publishing the event and failed
        /// </summary>
        PublishedFailed = 3
    }
}
