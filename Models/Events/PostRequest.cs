﻿using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Events
{
    public class PostRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}
