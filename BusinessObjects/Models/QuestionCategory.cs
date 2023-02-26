using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class QuestionCategory
    {
        public QuestionCategory()
        {
            Questions = new HashSet<Question>();
        }

        public byte Id { get; set; }
        public string Category { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
