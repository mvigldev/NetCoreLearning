using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreLearning.SchoolLessons.API.Database;
using NetCoreLearning.SchoolLessons.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreLearning.SchoolLessons.API.Controllers
{
    /// <summary>
    /// Controller for students
    /// </summary>
    /// <seealso cref="BaseController" />
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : BaseController
    {
        /// <summary>
        /// The context to access db
        /// </summary>
        private readonly SchoolLessonsContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentsController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="System.ArgumentNullException">context</exception>
        public StudentsController(SchoolLessonsContext context)
        {
            _context = context ?? throw new System.ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Gets the list of students.
        /// </summary>
        /// <returns>The students in the db</returns>
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Student>>> Get()
        {
            return await _context.Students.ToListAsync();
        }
    }
}