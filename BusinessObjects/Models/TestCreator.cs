using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class TestCreator
    {
        public TestCreator()
        {
            QuestionGroups = new HashSet<QuestionGroup>();
            Questions = new HashSet<Question>();
            Tests = new HashSet<Test>();
        }

        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<QuestionGroup> QuestionGroups { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
    }
}
