using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetCoreLearning.SchoolLessons.DomainModel;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreLearning.SchoolLessons.Worker
{
    /// <summary>
    /// background service that creates a Grade assignment evry 5 seconds
    /// </summary>
    /// <seealso cref="Microsoft.Extensions.Hosting.BackgroundService" />
    public class GradeAssignmentWorker : BackgroundService
    {
        private readonly ILogger<GradeAssignmentWorker> _logger;
        private readonly ISchoolLessonsService _schoolLessonsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GradeAssignmentWorker"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="schoolLessonsService">The school lessons service.</param>
        /// <exception cref="System.ArgumentNullException">
        /// logger
        /// or
        /// schoolLessonsService
        /// </exception>
        public GradeAssignmentWorker(ILogger<GradeAssignmentWorker> logger, ISchoolLessonsService schoolLessonsService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _schoolLessonsService = schoolLessonsService ?? throw new ArgumentNullException(nameof(schoolLessonsService));
        }

        /// <summary>
        /// This method is called when the <see cref="T:Microsoft.Extensions.Hosting.IHostedService" /> starts. The implementation should return a task that represents
        /// the lifetime of the long running operation(s) being performed.
        /// </summary>
        /// <param name="stoppingToken">Triggered when <see cref="M:Microsoft.Extensions.Hosting.IHostedService.StopAsync(System.Threading.CancellationToken)" /> is called.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> that represents the long running operations.
        /// </returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _schoolLessonsService.Authenticate();
            Random r = new Random();
            var students = await _schoolLessonsService.GetStudentsAsync();
            var professors = await _schoolLessonsService.GetProfessorsAsync();
            var lessons = await _schoolLessonsService.GetLessonsAsync();

            // every 30 sec create a random grade
            while (!stoppingToken.IsCancellationRequested)
            {
                var student = students.ElementAt(r.Next(0, students.Count()));
                var professor = professors.ElementAt(r.Next(0, professors.Count()));
                var lesson = lessons.ElementAt(r.Next(0, lessons.Count()));

                GradeAssignment gradeAssigment = new GradeAssignment()
                {
                    Id = Guid.NewGuid(),
                    LessonId = lesson.Id,
                    ProfessorId = professor.Id,
                    StudentId = student.Id,
                    Grade = GetRandomGrade(r)
                };

                _logger.LogDebug($"Adding new grade assignment Id:{gradeAssigment.Id}...");

                try
                {
                    await _schoolLessonsService.CreateGradeAssignmentAsync(gradeAssigment);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                }

                await Task.Delay(TimeSpan.FromSeconds(30));
            }
        }

        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
        /// <returns></returns>
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Worker started at: {DateTime.Now}");

            return base.StartAsync(cancellationToken);
        }

        private static string GetRandomGrade(Random r)
        {
            int x = r.Next(1, 5);
            if (x == 1) return "A";
            if (x == 2) return "B";
            if (x == 3) return "C";
            if (x == 4) return "D";
            if (x == 5) return "E";
            return "A";
        }
    }
}
