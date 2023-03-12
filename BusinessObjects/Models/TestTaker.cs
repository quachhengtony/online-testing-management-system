using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class TestTaker
    {
        public TestTaker()
        {
            Submissions = new HashSet<Submission>();
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Submission> Submissions { get; set; }
    }
}
