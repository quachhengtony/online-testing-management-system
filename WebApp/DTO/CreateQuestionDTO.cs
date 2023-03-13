using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.DTO
{
    public record CreateQuestionDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(1000, MinimumLength = 1)]
        public string Content { get; set; }

        [Required]
        [Range(1d, 100d)]
		public decimal Weight { get; set; }

		[Required]
        [DisplayName("Question Category")]
        [Range(1, 3)]
        public byte? QuestionCategoryId { get; set; }

        [Required]
        public Guid QuestionCreatorId { get; set; }

        [Required]
        public List<Answer> Answers { get; set; }
    }
}
