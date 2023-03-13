using BusinessObjects.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;

namespace WebApp.DTO
{
    public record UpdateQuestionDTO
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(1000, MinimumLength = 1)]
        public string Content { get; set; }

        [Required]
        [Range(1d, 100d)]
        public decimal Weight { get; set; }
    }
}
