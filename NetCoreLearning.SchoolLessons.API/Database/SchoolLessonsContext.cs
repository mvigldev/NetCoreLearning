using Microsoft.EntityFrameworkCore;
using NetCoreLearning.SchoolLessons.DomainModel;

namespace NetCoreLearning.SchoolLessons.API.Database
{
    /// <summary>
    /// The database context for the API
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class SchoolLessonsContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SchoolLessonsContext"/> class.
        /// </summary>
        /// <param name="options">The options for the context.</param>
        public SchoolLessonsContext(DbContextOptions<SchoolLessonsContext> options)
           : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the grade assignments.
        /// </summary>
        public DbSet<GradeAssignment> GradeAssignments { get; set; }

        /// <summary>
        /// Gets or sets the professors.
        /// </summary>
        public DbSet<Professor> Professors { get; set; }

        /// <summary>
        /// Gets or sets the lessons.
        /// </summary>
        public DbSet<Lesson> Lessons { get; set; }

        /// <summary>
        /// Gets or sets the students.
        /// </summary>
        public DbSet<Student> Students { get; set; }
    }
}
