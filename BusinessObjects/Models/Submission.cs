using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class Submission
    {
        public Guid Id { get; set; }
        public Guid TestTakerId { get; set; }
        public Guid TestId { get; set; }
        public DateTime SubmittedDate { get; set; }
        public DateTime GradedDate { get; set; }
        public byte TimeTaken { get; set; }
        public decimal Score { get; set; }
        public string Feedback { get; set; }
        public bool? IsGraded { get; set; }
        public string Content { get; set; }

        public virtual Test Test { get; set; }
        public virtual TestTaker TestTaker { get; set; }
    }
}
