using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class TestQuestion
    {
        public string QuestionId { get; set; }
        public string TestId { get; set; }

        public virtual Question Question { get; set; }
        public virtual Test Test { get; set; }
    }
}
