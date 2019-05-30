using Microsoft.AspNetCore.Mvc.RazorPages;
using NetCoreLearning.SchoolLessons.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreLearning.SchoolLessons.Razor.SignalR.Pages
{
    /// <summary>
    /// The model of Index view.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.RazorPages.PageModel" />
    public class IndexModel : PageModel
    {
        /// <summary>
        /// Gets the list of <see cref="GradeAssignment"/>s.
        /// </summary>
        public IList<GradeAssignment> GradeAssignments { get; private set; }

        /// <summary>
        /// on page load get empty list. To be filled by signal r javascript client live.
        /// </summary>
        /// <returns>Empty list of <see cref="GradeAssignment"/>s</returns>
        public async Task OnGet()
        {
            GradeAssignments = await GetMoqs();
        }

        private async Task<IList<GradeAssignment>> GetMoqs()
        {
            return await Task.FromResult<IList<GradeAssignment>>(new List<GradeAssignment>());
        }
    }
}
