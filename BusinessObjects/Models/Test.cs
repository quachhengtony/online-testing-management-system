using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class Test
    {
        public Test()
        {
            Submissions = new HashSet<Submission>();
            TestQuestions = new HashSet<TestQuestion>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string KeyCode { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime GradeReleaseDate { get; set; }
        public DateTime GradeFinalizationDate { get; set; }
        public byte Duration { get; set; }
        public string Batch { get; set; }
        public byte? TestCategoryId { get; set; }
        public Guid TestCreatorId { get; set; }

        public virtual TestCategory TestCategory { get; set; }
        public virtual TestCreator TestCreator { get; set; }
        public virtual ICollection<Submission> Submissions { get; set; }
        public virtual ICollection<TestQuestion> TestQuestions { get; set; }
    }
}
