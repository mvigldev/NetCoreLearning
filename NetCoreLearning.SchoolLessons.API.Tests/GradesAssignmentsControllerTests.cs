using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using NetCoreLearning.Infrastructure.EventBus.Database;
using NetCoreLearning.Infrastructure.EventBus.NServiceBus;
using NetCoreLearning.SchoolLessons.API.Controllers;
using NetCoreLearning.SchoolLessons.API.Database;
using NetCoreLearning.SchoolLessons.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NetCoreLearning.SchoolLessons.API.Tests
{
    public class GradesAssignmentsControllerTests
    {
        #region Constructor
        [Fact]
        public void Constructior_NullArguments()
        {
            Assert.Throws<ArgumentNullException>(() => { new GradesAssignmentsController(null, null, null, null, null, null); });
            var options = new DbContextOptionsBuilder<SchoolLessonsContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;
            var context = new SchoolLessonsContext(options);
            var eventBusMoq = new Mock<IEventBusPublisher<GradeAssignment>>(MockBehavior.Strict);
            var eventLogServiceFactoryMoq = new Mock<IEventLogServiceFactory>(MockBehavior.Strict);

            Assert.Throws<ArgumentNullException>(() => { new GradesAssignmentsController(context, null, null, null, null, null); });
            Assert.Throws<ArgumentNullException>(() => { new GradesAssignmentsController(context, Helper.GetLogger<GradesAssignmentsController>(), null, null, null, null); });
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GradesAssignmentsController(
                    context,
                    Helper.GetLogger<GradesAssignmentsController>(),
                    Helper.GetMemoryCache(), null, null, null);
            });
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GradesAssignmentsController(
                    context,
                    Helper.GetLogger<GradesAssignmentsController>(),
                    Helper.GetMemoryCache(), eventBusMoq.Object,
                    eventLogServiceFactoryMoq.Object, null);
            });
        }
        #endregion

        #region Get By Id
        [Fact]
        public async Task GetById_ReturnsCorrectItem()
        {
            #region Arrange 
            var options = new DbContextOptionsBuilder<SchoolLessonsContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString())
                       .Options;
            var context = new SchoolLessonsContext(options);

            GradeAssignment gradeAssigmentToGet = new GradeAssignment()
            {
                Id = Guid.NewGuid(),
                ProfessorId = Guid.NewGuid(),
                StudentId = Guid.NewGuid(),
                LessonId = Guid.NewGuid(),
                Grade = "B"
            };

            List<GradeAssignment> gradeAssigments = new List<GradeAssignment>()
            {
                new GradeAssignment()
            {
                Id = Guid.NewGuid(),
                ProfessorId = Guid.NewGuid(),
                StudentId = Guid.NewGuid(),
                LessonId = Guid.NewGuid(),
                Grade = "A"
            },
                gradeAssigmentToGet,
                new GradeAssignment()
            {
                Id = Guid.NewGuid(),
                ProfessorId = Guid.NewGuid(),
                StudentId = Guid.NewGuid(),
                LessonId = Guid.NewGuid(),
                Grade = "C"
            }
            };

            context.GradeAssignments.AddRange(gradeAssigments);
            context.SaveChanges();

            var eventBusMoq = new Mock<IEventBusPublisher<GradeAssignment>>(MockBehavior.Strict);
            var eventLogServiceFactoryMoq = new Mock<IEventLogServiceFactory>(MockBehavior.Strict);
            var eventLogServiceMoq = new Mock<IEventLogService>(MockBehavior.Strict);
            var resilientTransactionMoq = new Mock<IResilientTransaction>(MockBehavior.Strict);
            eventLogServiceFactoryMoq.Setup(c => c.CreateEventLogService(context)).Returns(eventLogServiceMoq.Object);
            #endregion

            // Act
            var controller = new GradesAssignmentsController(
               context,
               Helper.GetLogger<GradesAssignmentsController>(),
               Helper.GetMemoryCache(),
               eventBusMoq.Object,
               eventLogServiceFactoryMoq.Object,
               resilientTransactionMoq.Object);
            var actionResult = await controller.Get(gradeAssigmentToGet.Id) as ActionResult<GradeAssignment>;

            // Assert
            Assert.Equal(gradeAssigmentToGet.Id, actionResult.Value.Id);
            eventBusMoq.VerifyAll();
            eventLogServiceFactoryMoq.VerifyAll();
            eventLogServiceMoq.VerifyAll();
            resilientTransactionMoq.VerifyAll();
        }

        [Fact]
        public async Task GetById_ReturnsNotFoundResult()
        {
            #region Arrange 
            var options = new DbContextOptionsBuilder<SchoolLessonsContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString())
                       .Options;
            var context = new SchoolLessonsContext(options);

            GradeAssignment gradeAssigmentToGetNotExisting = new GradeAssignment()
            {
                Id = Guid.NewGuid(),
                ProfessorId = Guid.NewGuid(),
                StudentId = Guid.NewGuid(),
                LessonId = Guid.NewGuid(),
                Grade = "B"
            };

            List<GradeAssignment> gradeAssigments = new List<GradeAssignment>()
            {
                new GradeAssignment()
            {
                Id = Guid.NewGuid(),
                ProfessorId = Guid.NewGuid(),
                StudentId = Guid.NewGuid(),
                LessonId = Guid.NewGuid(),
                Grade = "A"
            },
                new GradeAssignment()
            {
                Id = Guid.NewGuid(),
                ProfessorId = Guid.NewGuid(),
                StudentId = Guid.NewGuid(),
                LessonId = Guid.NewGuid(),
                Grade = "C"
            }
            };

            context.GradeAssignments.AddRange(gradeAssigments);
            context.SaveChanges();

            var eventBusMoq = new Mock<IEventBusPublisher<GradeAssignment>>(MockBehavior.Strict);
            var eventLogServiceFactoryMoq = new Mock<IEventLogServiceFactory>(MockBehavior.Strict);
            var eventLogServiceMoq = new Mock<IEventLogService>(MockBehavior.Strict);
            var resilientTransactionMoq = new Mock<IResilientTransaction>(MockBehavior.Strict);
            eventLogServiceFactoryMoq.Setup(c => c.CreateEventLogService(context)).Returns(eventLogServiceMoq.Object);
            #endregion

            // Act
            var controller = new GradesAssignmentsController(
               context,
               Helper.GetLogger<GradesAssignmentsController>(),
               Helper.GetMemoryCache(),
               eventBusMoq.Object,
               eventLogServiceFactoryMoq.Object,
               resilientTransactionMoq.Object);
            var actionResult = await controller.Get(gradeAssigmentToGetNotExisting.Id);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult.Result);
            eventBusMoq.VerifyAll();
            eventLogServiceFactoryMoq.VerifyAll();
            eventLogServiceMoq.VerifyAll();
            resilientTransactionMoq.VerifyAll();
        }
        #endregion

        #region Post
        /// <summary>
        /// Initializes the cache first time only.
        /// </summary>
        /// <param name="context">The context to access db.</param>
        /// <param name="cache">The cache to init.</param>
        private void InitCache(SchoolLessonsContext context, IMemoryCache cache)
        {
            cache.GetOrCreate("Students", entry =>
            {
                return context.Students.ToDictionary(c => c.Id, c => c);
            });

            cache.GetOrCreate("Lessons", entry =>
            {
                return context.Lessons.ToDictionary(c => c.Id, c => c);
            });

            cache.GetOrCreate("Professors", entry =>
            {
                return context.Professors.ToDictionary(c => c.Id, c => c);
            });
        }

        [Fact]
        public async Task PostValidObject_ReturnsCreatedResponse()
        {
            #region Arrange 
            var options = new DbContextOptionsBuilder<SchoolLessonsContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString())
                       .Options;
            var context = new SchoolLessonsContext(options);

            Professor professor = new Professor() { Id = Guid.NewGuid(), Name = "Professor" };
            context.Professors.Add(professor);
            Student student = new Student() { Id = Guid.NewGuid(), Name = "Student" };
            context.Students.Add(student);
            Lesson lesson1 = new Lesson() { Id = Guid.NewGuid(), Name = "Lesson 1" };
            Lesson lesson2 = new Lesson() { Id = Guid.NewGuid(), Name = "Lesson 2" };
            Lesson lesson3 = new Lesson() { Id = Guid.NewGuid(), Name = "Lesson 3" };
            context.Lessons.AddRange(lesson1, lesson2, lesson3);

            GradeAssignment gradeAssigmentToAdd = new GradeAssignment()
            {
                Id = Guid.NewGuid(),
                ProfessorId = professor.Id,
                StudentId = student.Id,
                LessonId = lesson2.Id,
                Grade = "B"
            };

            List<GradeAssignment> gradeAssigments = new List<GradeAssignment>()
            {
                new GradeAssignment()
            {
                Id = Guid.NewGuid(),
                ProfessorId =professor.Id,
                StudentId = student.Id,
                LessonId = lesson1.Id,
                Grade = "A"
            },
                new GradeAssignment()
            {
                Id = Guid.NewGuid(),
                ProfessorId = professor.Id,
                StudentId = student.Id,
                LessonId = lesson3.Id,
                Grade = "C"
            }
            };

            context.GradeAssignments.AddRange(gradeAssigments);
            context.SaveChanges();

            var cache = Helper.GetMemoryCache();
            InitCache(context, cache);

            var eventBusMoq = new Mock<IEventBusPublisher<GradeAssignment>>(MockBehavior.Strict);
            var eventLogServiceFactoryMoq = new Mock<IEventLogServiceFactory>(MockBehavior.Strict);
            var eventLogServiceMoq = new Mock<IEventLogService>(MockBehavior.Strict);
            var resilientTransactionMoq = new Mock<IResilientTransaction>(MockBehavior.Strict);
            eventLogServiceFactoryMoq.Setup(c => c.CreateEventLogService(context)).Returns(eventLogServiceMoq.Object);
            eventLogServiceMoq.Setup(c => c.MarkEventAsInProgressAsync(It.IsAny<Guid>())).Returns(Task.CompletedTask);
            eventLogServiceMoq.Setup(c => c.MarkEventAsPublishedAsync(It.IsAny<Guid>())).Returns(Task.CompletedTask);
            eventBusMoq.Setup(c => c.PublishAsync(It.IsAny<Event<GradeAssignment>>())).Returns(Task.CompletedTask);
            resilientTransactionMoq.Setup(c => c.ExecuteAsync(context, It.IsAny<Func<Task>>())).Returns(Task.CompletedTask);

            #endregion

            // Act
            var controller = new GradesAssignmentsController(
              context,
              Helper.GetLogger<GradesAssignmentsController>(),
              cache,
              eventBusMoq.Object,
              eventLogServiceFactoryMoq.Object,
              resilientTransactionMoq.Object);
            var actionResult = await controller.Post(gradeAssigmentToAdd) as CreatedAtActionResult;

            // Assert
            actionResult = actionResult as CreatedAtActionResult;
            var createdTeam = actionResult.Value as GradeAssignment;
            Assert.Equal(gradeAssigmentToAdd.Id, createdTeam.Id);
            eventBusMoq.VerifyAll();
            eventLogServiceFactoryMoq.VerifyAll();
            eventLogServiceMoq.VerifyAll();
            resilientTransactionMoq.VerifyAll();
        }

        [Fact]
        public async Task PostInvalidLessonId_ReturnsBadRequestResponse()
        {
            #region Arrange 
            var options = new DbContextOptionsBuilder<SchoolLessonsContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString())
                       .Options;
            var context = new SchoolLessonsContext(options);

            Professor professor = new Professor() { Id = Guid.NewGuid(), Name = "Professor" };
            context.Professors.Add(professor);
            Student student = new Student() { Id = Guid.NewGuid(), Name = "Student" };
            context.Students.Add(student);
            Lesson lesson1 = new Lesson() { Id = Guid.NewGuid(), Name = "Lesson 1" };
            Lesson lesson2 = new Lesson() { Id = Guid.NewGuid(), Name = "Lesson 2" };
            Lesson lesson3 = new Lesson() { Id = Guid.NewGuid(), Name = "Lesson 3" };
            context.Lessons.AddRange(lesson1, lesson2, lesson3);

            GradeAssignment gradeAssigmentToAdd = new GradeAssignment()
            {
                Id = Guid.NewGuid(),
                ProfessorId = professor.Id,
                StudentId = student.Id,
                LessonId = Guid.NewGuid(),// this will not be found
                Grade = "B"
            };

            List<GradeAssignment> gradeAssigments = new List<GradeAssignment>()
            {
                new GradeAssignment()
            {
                Id = Guid.NewGuid(),
                ProfessorId =professor.Id,
                StudentId = student.Id,
                LessonId = lesson1.Id,
                Grade = "A"
            },
                new GradeAssignment()
            {
                Id = Guid.NewGuid(),
                ProfessorId = professor.Id,
                StudentId = student.Id,
                LessonId = lesson3.Id,
                Grade = "C"
            }
            };

            context.GradeAssignments.AddRange(gradeAssigments);
            context.SaveChanges();

            var cache = Helper.GetMemoryCache();
            InitCache(context, cache);

            var eventBusMoq = new Mock<IEventBusPublisher<GradeAssignment>>(MockBehavior.Strict);
            var eventLogServiceFactoryMoq = new Mock<IEventLogServiceFactory>(MockBehavior.Strict);
            var eventLogServiceMoq = new Mock<IEventLogService>(MockBehavior.Strict);
            var resilientTransactionMoq = new Mock<IResilientTransaction>(MockBehavior.Strict);
            eventLogServiceFactoryMoq.Setup(c => c.CreateEventLogService(context)).Returns(eventLogServiceMoq.Object);

            #endregion

            // Act
            var controller = new GradesAssignmentsController(
            context,
            Helper.GetLogger<GradesAssignmentsController>(),
            cache,
            eventBusMoq.Object,
            eventLogServiceFactoryMoq.Object,
            resilientTransactionMoq.Object);
            var actionResult = await controller.Post(gradeAssigmentToAdd);

            // Assert
            var badRequestResult = actionResult as BadRequestObjectResult;
            Assert.Equal($"Lesson with id {gradeAssigmentToAdd.LessonId} was not found.", badRequestResult.Value);
            eventBusMoq.VerifyAll();
            eventLogServiceFactoryMoq.VerifyAll();
            eventLogServiceMoq.VerifyAll();
            resilientTransactionMoq.VerifyAll();
        }

        [Fact]
        public async Task PostInvalidProfessorId_ReturnsBadRequestResponse()
        {
            #region Arrange 
            var options = new DbContextOptionsBuilder<SchoolLessonsContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString())
                       .Options;
            var context = new SchoolLessonsContext(options);

            Professor professor = new Professor() { Id = Guid.NewGuid(), Name = "Professor" };
            context.Professors.Add(professor);
            Student student = new Student() { Id = Guid.NewGuid(), Name = "Student" };
            context.Students.Add(student);
            Lesson lesson1 = new Lesson() { Id = Guid.NewGuid(), Name = "Lesson 1" };
            Lesson lesson2 = new Lesson() { Id = Guid.NewGuid(), Name = "Lesson 2" };
            Lesson lesson3 = new Lesson() { Id = Guid.NewGuid(), Name = "Lesson 3" };
            context.Lessons.AddRange(lesson1, lesson2, lesson3);

            GradeAssignment gradeAssigmentToAdd = new GradeAssignment()
            {
                Id = Guid.NewGuid(),
                ProfessorId = Guid.NewGuid(),// this will not be found
                StudentId = student.Id,
                LessonId = lesson2.Id,
                Grade = "B"
            };

            List<GradeAssignment> gradeAssigments = new List<GradeAssignment>()
            {
                new GradeAssignment()
            {
                Id = Guid.NewGuid(),
                ProfessorId =professor.Id,
                StudentId = student.Id,
                LessonId = lesson1.Id,
                Grade = "A"
            },
                new GradeAssignment()
            {
                Id = Guid.NewGuid(),
                ProfessorId = professor.Id,
                StudentId = student.Id,
                LessonId = lesson3.Id,
                Grade = "C"
            }
            };

            context.GradeAssignments.AddRange(gradeAssigments);
            context.SaveChanges();

            var cache = Helper.GetMemoryCache();
            InitCache(context, cache);

            var eventBusMoq = new Mock<IEventBusPublisher<GradeAssignment>>(MockBehavior.Strict);
            var eventLogServiceFactoryMoq = new Mock<IEventLogServiceFactory>(MockBehavior.Strict);
            var eventLogServiceMoq = new Mock<IEventLogService>(MockBehavior.Strict);
            var resilientTransactionMoq = new Mock<IResilientTransaction>(MockBehavior.Strict);
            eventLogServiceFactoryMoq.Setup(c => c.CreateEventLogService(context)).Returns(eventLogServiceMoq.Object);

            #endregion

            // Act
            var controller = new GradesAssignmentsController(
             context,
             Helper.GetLogger<GradesAssignmentsController>(),
             cache,
             eventBusMoq.Object,
             eventLogServiceFactoryMoq.Object,
             resilientTransactionMoq.Object);
            var actionResult = await controller.Post(gradeAssigmentToAdd);

            // Assert
            var badRequestResult = actionResult as BadRequestObjectResult;
            Assert.Equal($"Professor with id {gradeAssigmentToAdd.ProfessorId} was not found.", badRequestResult.Value);
            eventBusMoq.VerifyAll();
            eventLogServiceFactoryMoq.VerifyAll();
            eventLogServiceMoq.VerifyAll();
            resilientTransactionMoq.VerifyAll();
        }

        [Fact]
        public async Task PostInvalidStudentId_ReturnsBadRequestResponse()
        {
            #region Arrange 
            var options = new DbContextOptionsBuilder<SchoolLessonsContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString())
                       .Options;
            var context = new SchoolLessonsContext(options);

            Professor professor = new Professor() { Id = Guid.NewGuid(), Name = "Professor" };
            context.Professors.Add(professor);
            Student student = new Student() { Id = Guid.NewGuid(), Name = "Student" };
            context.Students.Add(student);
            Lesson lesson1 = new Lesson() { Id = Guid.NewGuid(), Name = "Lesson 1" };
            Lesson lesson2 = new Lesson() { Id = Guid.NewGuid(), Name = "Lesson 2" };
            Lesson lesson3 = new Lesson() { Id = Guid.NewGuid(), Name = "Lesson 3" };
            context.Lessons.AddRange(lesson1, lesson2, lesson3);

            GradeAssignment gradeAssigmentToAdd = new GradeAssignment()
            {
                Id = Guid.NewGuid(),
                ProfessorId = professor.Id,
                StudentId = Guid.NewGuid(),// this will not be found
                LessonId = lesson2.Id,
                Grade = "B"
            };

            List<GradeAssignment> gradeAssigments = new List<GradeAssignment>()
            {
                new GradeAssignment()
            {
                Id = Guid.NewGuid(),
                ProfessorId =professor.Id,
                StudentId = student.Id,
                LessonId = lesson1.Id,
                Grade = "A"
            },
                new GradeAssignment()
            {
                Id = Guid.NewGuid(),
                ProfessorId = professor.Id,
                StudentId = student.Id,
                LessonId = lesson3.Id,
                Grade = "C"
            }
            };

            context.GradeAssignments.AddRange(gradeAssigments);
            context.SaveChanges();

            var cache = Helper.GetMemoryCache();
            InitCache(context, cache);

            var eventBusMoq = new Mock<IEventBusPublisher<GradeAssignment>>(MockBehavior.Strict);
            var eventLogServiceFactoryMoq = new Mock<IEventLogServiceFactory>(MockBehavior.Strict);
            var eventLogServiceMoq = new Mock<IEventLogService>(MockBehavior.Strict);
            var resilientTransactionMoq = new Mock<IResilientTransaction>(MockBehavior.Strict);
            eventLogServiceFactoryMoq.Setup(c => c.CreateEventLogService(context)).Returns(eventLogServiceMoq.Object);

            #endregion

            // Act
            var controller = new GradesAssignmentsController(
            context,
            Helper.GetLogger<GradesAssignmentsController>(),
            cache,
            eventBusMoq.Object,
            eventLogServiceFactoryMoq.Object,
            resilientTransactionMoq.Object);
            var actionResult = await controller.Post(gradeAssigmentToAdd);

            // Assert
            var badRequestResult = actionResult as BadRequestObjectResult;
            Assert.Equal($"Student with id {gradeAssigmentToAdd.StudentId} was not found.", badRequestResult.Value);
            eventBusMoq.VerifyAll();
            eventLogServiceFactoryMoq.VerifyAll();
            eventLogServiceMoq.VerifyAll();
            resilientTransactionMoq.VerifyAll();
        }

        [Fact]
        public async Task PostValidObject_ThrowsErrorEventBus()
        {
            #region Arrange 
            var options = new DbContextOptionsBuilder<SchoolLessonsContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString())
                       .Options;
            var context = new SchoolLessonsContext(options);

            Professor professor = new Professor() { Id = Guid.NewGuid(), Name = "Professor" };
            context.Professors.Add(professor);
            Student student = new Student() { Id = Guid.NewGuid(), Name = "Student" };
            context.Students.Add(student);
            Lesson lesson1 = new Lesson() { Id = Guid.NewGuid(), Name = "Lesson 1" };
            Lesson lesson2 = new Lesson() { Id = Guid.NewGuid(), Name = "Lesson 2" };
            Lesson lesson3 = new Lesson() { Id = Guid.NewGuid(), Name = "Lesson 3" };
            context.Lessons.AddRange(lesson1, lesson2, lesson3);

            GradeAssignment gradeAssigmentToAdd = new GradeAssignment()
            {
                Id = Guid.NewGuid(),
                ProfessorId = professor.Id,
                StudentId = student.Id,
                LessonId = lesson2.Id,
                Grade = "B"
            };

            List<GradeAssignment> gradeAssigments = new List<GradeAssignment>()
            {
                new GradeAssignment()
            {
                Id = Guid.NewGuid(),
                ProfessorId =professor.Id,
                StudentId = student.Id,
                LessonId = lesson1.Id,
                Grade = "A"
            },
                new GradeAssignment()
            {
                Id = Guid.NewGuid(),
                ProfessorId = professor.Id,
                StudentId = student.Id,
                LessonId = lesson3.Id,
                Grade = "C"
            }
            };

            context.GradeAssignments.AddRange(gradeAssigments);
            context.SaveChanges();

            var cache = Helper.GetMemoryCache();
            InitCache(context, cache);

            var eventBusMoq = new Mock<IEventBusPublisher<GradeAssignment>>(MockBehavior.Strict);
            var eventLogServiceFactoryMoq = new Mock<IEventLogServiceFactory>(MockBehavior.Strict);
            var eventLogServiceMoq = new Mock<IEventLogService>(MockBehavior.Strict);
            var resilientTransactionMoq = new Mock<IResilientTransaction>(MockBehavior.Strict);
            eventLogServiceFactoryMoq.Setup(c => c.CreateEventLogService(context)).Returns(eventLogServiceMoq.Object);
            eventLogServiceMoq.Setup(c => c.MarkEventAsInProgressAsync(It.IsAny<Guid>())).Returns(Task.CompletedTask);
            eventLogServiceMoq.Setup(c => c.MarkEventAsFailedAsync(It.IsAny<Guid>())).Returns(Task.CompletedTask);
            resilientTransactionMoq.Setup(c => c.ExecuteAsync(context, It.IsAny<Func<Task>>())).Returns(Task.CompletedTask);

            #endregion

            // Act
            var controller = new GradesAssignmentsController(
              context,
              Helper.GetLogger<GradesAssignmentsController>(),
              cache,
              eventBusMoq.Object,
              eventLogServiceFactoryMoq.Object,
              resilientTransactionMoq.Object);

            // Assert
            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await ControllerThrowsEventBusException(controller, gradeAssigmentToAdd, eventBusMoq));

            Assert.Equal("Event bus unavailable", ex.Message);
            eventBusMoq.VerifyAll();
            eventLogServiceFactoryMoq.VerifyAll();
            eventLogServiceMoq.VerifyAll();
            resilientTransactionMoq.VerifyAll();
        }

        private async Task ControllerThrowsEventBusException(
            GradesAssignmentsController controller,
            GradeAssignment gradeAssigmentToAdd,
            Mock<IEventBusPublisher<GradeAssignment>> eventBusMoq)
        {
            eventBusMoq.Setup(c => c.PublishAsync(It.IsAny<Event<GradeAssignment>>())).Throws(new Exception("Event bus unavailable"));
            await controller.Post(gradeAssigmentToAdd);
        }


        #endregion
    }
}
