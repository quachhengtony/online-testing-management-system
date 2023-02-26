using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class TestQuestionGroup
    {
        public string QuestionGroupId { get; set; }
        public string TestId { get; set; }

        public virtual QuestionGroup QuestionGroup { get; set; }
        public virtual Test Test { get; set; }
    }
}
