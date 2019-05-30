using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreLearning.SchoolLessons.API.Database;
using NetCoreLearning.SchoolLessons.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreLearning.SchoolLessons.API.Controllers
{
    /// <summary>
    /// Controller for Lessons
    /// </summary>
    /// <seealso cref="BaseController" />
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : BaseController
    {
        /// <summary>
        /// The context to access db
        /// </summary>
        private readonly SchoolLessonsContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="LessonsController"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="System.ArgumentNullException">context</exception>
        public LessonsController(SchoolLessonsContext context)
        {
            _context = context ?? throw new System.ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Gets the list of lessons.
        /// </summary>
        /// <returns>The lessons in the db</returns>
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Lesson>>> Get()
        {
            return await _context.Lessons.ToListAsync();
        }
    }
}