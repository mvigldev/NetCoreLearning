using NetCoreLearning.SchoolLessons.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreLearning.SchoolLessons.Razor.SignalR
{
    /// <summary>
    /// Interface for client methods
    /// </summary>
    public interface IShowLiveGradeAssignments
    {
        /// <summary>
        /// Shows the live grade assignments.
        /// </summary>
        /// <param name="liveGradeAssignments">The live grade assignments.</param>
        /// <returns>The list of live grade assignments</returns>
        Task ShowLiveGradeAssignments(IEnumerable<GradeAssignment> liveGradeAssignments);

        /// <summary>
        /// Resets the live grade assignments.
        /// </summary>
        /// <returns>task to reset the grade assignments</returns>
        Task ResetLiveGradeAssignments();
    }
}