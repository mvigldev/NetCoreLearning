using Microsoft.Extensions.Caching.Distributed;
using NetCoreLearning.SchoolLessons.DomainModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreLearning.SchoolLessons.Razor.SignalR.Hubs
{
    /// <summary>
    /// The signalr hub.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.SignalR.Hub{T}" />
    public class GradeAssignmentsHub : Microsoft.AspNetCore.SignalR.Hub<IShowLiveGradeAssignments>
    {
        private readonly IDistributedCache _cache;
        /// <summary>
        /// Initializes a new instance of the <see cref="GradeAssignmentsHub"/> class.
        /// </summary>
        /// <param name="cache">The cache.</param>
        /// <exception cref="System.ArgumentNullException">cache</exception>
        public GradeAssignmentsHub(IDistributedCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        /// <summary>
        /// Sends the live grade assignments to client. Only when the client asks(usually on connect).
        /// </summary>
        /// <returns>A task with all the grade assignments in the distributed cache</returns>
        public async Task SendLiveGradeAssignmentsToClient()
        {
            var liveGradeAssignments = await GetCachedLiveGradeAssignments();
            await Clients.Caller.ShowLiveGradeAssignments(liveGradeAssignments);
        }

        private async Task<IEnumerable<GradeAssignment>> GetCachedLiveGradeAssignments()
        {
            string cachedLiveGradeAssignments = await _cache.GetStringAsync("GradeAssignments");
            if (cachedLiveGradeAssignments == null)
            {
                return Enumerable.Empty<GradeAssignment>();
            }
            return JsonConvert.DeserializeObject<Dictionary<Guid, GradeAssignment>>(cachedLiveGradeAssignments).Select(c => c.Value);
        }
    }
}
