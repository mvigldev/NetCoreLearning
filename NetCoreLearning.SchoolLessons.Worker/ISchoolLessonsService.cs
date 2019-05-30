using NetCoreLearning.SchoolLessons.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreLearning.SchoolLessons.Worker
{
    /// <summary>
    /// Service contract to access the SchoolLessos API
    /// </summary>
    public interface ISchoolLessonsService
    {
        /// <summary>
        /// Authenticates to the API.
        /// </summary>
        Task Authenticate();

        /// <summary>
        /// Creates the grade assignment asynchronous.
        /// </summary>
        /// <param name="gradeAssigment">The grade assignment.</param>
        /// <returns></returns>
        Task CreateGradeAssignmentAsync(GradeAssignment gradeAssigment);

        /// <summary>
        /// Gets the students asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Student>> GetStudentsAsync();

        /// <summary>
        /// Gets the lessons asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Lesson>> GetLessonsAsync();

        /// <summary>
        /// Gets the professors asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Professor>> GetProfessorsAsync();
    }
}