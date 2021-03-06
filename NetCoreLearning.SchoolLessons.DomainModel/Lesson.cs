﻿using System;
using System.ComponentModel.DataAnnotations;

namespace NetCoreLearning.SchoolLessons.DomainModel
{
    /// <summary>
    /// Represents a lesson
    /// </summary>
    public class Lesson
    {
        /// <summary>
        /// Gets or sets the Lesson database identifier. Auto-generated by the API.
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Lesson name.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
