using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class Answer
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
        public string QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}
