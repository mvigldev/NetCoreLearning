using Microsoft.Extensions.Caching.Distributed;
using NetCoreLearning.Infrastructure.EventBus.NServiceBus;
using NetCoreLearning.SchoolLessons.DomainModel;
using NServiceBus;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

namespace NetCoreLearning.SchoolLessons.Worker
{
    /// <summary>
    /// The event handler for an added GradeAssignment. 
    /// </summary>
    /// <seealso cref="EventBusSubscriber{GradeAssignment}"/>
    public class GradeAssignmentEventHandler : EventBusSubscriber<GradeAssignment>
    {
        /// <summary>
        /// Handles the specified event:Adds in distributed cache the value. This value expires at the end of the day.
        /// </summary>
        /// <param name="event">The event to handle.</param>
        /// <param name="context">The <see cref="T:NServiceBus.IMessageHandlerContext" /> with the info of the event bus.</param>
        /// <returns>
        /// A task that handles the specified event.
        /// </returns>
        public override async Task Handle(Event<GradeAssignment> @event, IMessageHandlerContext context)
        {
            // this is called only when the job is finished, no concurrent messages. Thus it can use a static method with safe.
            context.DoNotContinueDispatchingCurrentMessageToHandlers();
            var cache = Program.ServiceProviderInstance.GetService<IDistributedCache>();
            var cachedGradeAssignments = await GetCachedGradeAssignments(cache);
            cachedGradeAssignments.Add(@event.Model.Id, @event.Model);
            await cache.SetStringAsync("GradeAssignments", JsonConvert.SerializeObject(cachedGradeAssignments),
               new DistributedCacheEntryOptions().SetAbsoluteExpiration((DateTime.Now.AddDays(1).Date) - DateTime.Now));
        }

        private async Task<Dictionary<Guid, GradeAssignment>> GetCachedGradeAssignments(IDistributedCache cache)
        {
            string cachedGradeAssignments = await cache.GetStringAsync("GradeAssignments");
            if (cachedGradeAssignments == null)
            {
                return new Dictionary<Guid, GradeAssignment>();
            }
            return JsonConvert.DeserializeObject<Dictionary<Guid, GradeAssignment>>(cachedGradeAssignments);
        }
    }
}
