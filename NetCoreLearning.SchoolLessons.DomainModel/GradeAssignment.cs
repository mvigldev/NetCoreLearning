﻿using System;
using System.ComponentModel.DataAnnotations;

namespace NetCoreLearning.SchoolLessons.DomainModel
{
    /// <summary>
    /// Represents a grade assignment that a professor assigned to a student for a lesson.
    /// </summary>
    public class GradeAssignment
    {
        /// <summary>
        /// Gets or sets the database identifier of the grade assignment. Auto generated by the API.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Professor database identifier.
        /// </summary>
        [Required]
        public Guid ProfessorId { get; set; }

        /// <summary>
        /// Gets or sets the Student database identifier.
        /// </summary>
        [Required]
        public Guid StudentId { get; set; }

        /// <summary>
        /// Gets or sets the Lesson database identifier.
        /// </summary>
        [Required]
        public Guid LessonId { get; set; }

        /// <summary>
        /// Gets or sets the grade that the professor assigned to the student for the lesson.
        /// </summary>
        [Required]
        [MaxLength(1)]
        [RegularExpression("^[A-E]{1,1}$")]
        public string Grade { get; set; }
    }
}