using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using NetCoreLearning.Infrastructure.EventBus.Database;
using NetCoreLearning.Infrastructure.EventBus.NServiceBus;
using NetCoreLearning.SchoolLessons.API.Database;
using NetCoreLearning.SchoolLessons.DomainModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreLearning.SchoolLessons.API.Controllers
{
    /// <summary>
    /// Grades AssigmentsController endpoint
    /// </summary>
    /// <seealso cref="BaseController" />
    public class GradesAssignmentsController : BaseController
    {
        private readonly SchoolLessonsContext _context;
        private readonly ILogger<GradesAssignmentsController> _logger;
        private readonly IMemoryCache _cache;
        private readonly IEventBusPublisher<GradeAssignment> _eventBus;
        private readonly IEventLogService _eventLogService;
        private readonly IResilientTransaction _resilientTransaction;

        /// <summary>
        /// Initializes a new instance of the <see cref="GradesAssignmentsController" /> class.
        /// </summary>
        /// <param name="context">The context to store the data.</param>
        /// <param name="logger">The logger for logging.</param>
        /// <param name="cache">The cache.</param>
        /// <param name="eventBus">The  event publisher.</param>
        /// <param name="eventLogServiceFactory">The event log service factory.</param>
        /// <param name="resilientTransaction">The transaction to have atomicity between publish event and save changes.</param>
        /// <exception cref="System.ArgumentNullException">context
        /// or
        /// logger</exception>
        public GradesAssignmentsController(
            SchoolLessonsContext context,
            ILogger<GradesAssignmentsController> logger,
            IMemoryCache cache,
            IEventBusPublisher<GradeAssignment> eventBus,
            IEventLogServiceFactory eventLogServiceFactory,
            IResilientTransaction resilientTransaction)
        {
            _context = context ?? throw new System.ArgumentNullException(nameof(context));
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
            _cache = cache ?? throw new System.ArgumentNullException(nameof(cache));
            _eventBus = eventBus ?? throw new System.ArgumentNullException(nameof(eventBus));
            _resilientTransaction = resilientTransaction ?? throw new System.ArgumentNullException(nameof(resilientTransaction));
            if (eventLogServiceFactory == null) throw new System.ArgumentNullException(nameof(eventLogServiceFactory));

            _eventLogService = eventLogServiceFactory.CreateEventLogService(_context);
        }

        /// <summary>
        /// Gets the specified grade assignment.
        /// </summary>
        /// <param name="id">The database identifier of the grade assignment.</param>
        /// <returns>The <see cref="GradeAssignment"/> found</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GradeAssignment>> Get(Guid id)
        {
            _logger.LogDebug("Looking for grade assignment...");

            GradeAssignment team = await _context.GradeAssignments.FindAsync(id);

            if (team == null)
            {
                _logger.LogWarning("Grade assignment not found.");
                return NotFound();
            }

            return team;
        }

        /// <summary>
        /// Creates the specified grade assignment.
        /// </summary>
        /// <param name="gradeAssignment">The grade assignment to create in the database.</param>
        /// <returns>The created <see cref="GradeAssignment"/> </returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GradeAssignment gradeAssignment)
        {
            _logger.LogDebug("new grade assignment...");

            _cache.TryGetValue("Students", out object studentsObj);
            Dictionary<Guid, Student> students = (Dictionary<Guid, Student>)studentsObj;

            #region Find related items
            if (!students.ContainsKey(gradeAssignment.StudentId))
            {
                string errorMessage = $"Student with id {gradeAssignment.StudentId} was not found.";
                _logger.LogError(errorMessage);
                return BadRequest(errorMessage);
            }

            _cache.TryGetValue("Lessons", out object lesonnsObj);
            Dictionary<Guid, Lesson> lesonns = (Dictionary<Guid, Lesson>)lesonnsObj;

            if (!lesonns.ContainsKey(gradeAssignment.LessonId))
            {
                string errorMessage = $"Lesson with id {gradeAssignment.LessonId} was not found.";
                _logger.LogError(errorMessage);
                return BadRequest(errorMessage);
            }

            _cache.TryGetValue("Professors", out object professorsObj);
            Dictionary<Guid, Professor> professors = (Dictionary<Guid, Professor>)professorsObj;

            if (!professors.ContainsKey(gradeAssignment.ProfessorId))
            {
                string errorMessage = $"Professor with id {gradeAssignment.ProfessorId} was not found.";
                _logger.LogError(errorMessage);
                return BadRequest(errorMessage);
            }
            #endregion

            gradeAssignment.Id = Guid.NewGuid();
            await _context.GradeAssignments.AddAsync(gradeAssignment);

            Event<GradeAssignment> newEvent = new Event<GradeAssignment>()
            {
                Model = gradeAssignment
            };

            await _resilientTransaction.ExecuteAsync(_context, async () =>
             {
                 await _context.SaveChangesAsync();
                 await _eventLogService.SaveEventAsync(newEvent, _context.Database.CurrentTransaction.GetDbTransaction());
             });

            try
            {
                await _eventLogService.MarkEventAsInProgressAsync(newEvent.Id);
                await _eventBus.PublishAsync(newEvent);
                await _eventLogService.MarkEventAsPublishedAsync(newEvent.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                await _eventLogService.MarkEventAsFailedAsync(newEvent.Id);
                throw;
            }

            return CreatedAtAction(nameof(Get), new
            {
                id = gradeAssignment.Id
            }, gradeAssignment);
        }
    }
}
