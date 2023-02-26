using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class QuestionGroupQuestion
    {
        public string QuestionGroupId { get; set; }
        public string QuestionId { get; set; }

        public virtual Question Question { get; set; }
        public virtual QuestionGroup QuestionGroup { get; set; }
    }
}
