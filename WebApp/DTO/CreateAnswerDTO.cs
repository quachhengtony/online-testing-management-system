using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.DTO
{
    public record CreateAnswerDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Content { get; set; }
        [Required]
        [DisplayName("Is Correct")]
        public bool IsCorrect { get; set; }
        [Required]
        public string QuestionId { get; set; }
    }
}
