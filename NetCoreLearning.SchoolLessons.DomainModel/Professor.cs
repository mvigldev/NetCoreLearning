﻿using System;
using System.ComponentModel.DataAnnotations;

namespace NetCoreLearning.SchoolLessons.DomainModel
{
    /// <summary>
    /// Represents a professor
    /// </summary>
    public class Professor
    {
        /// <summary>
        /// Gets or sets the Professor database identifier. Auto-generated by the API.
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Professor name.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}