using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class TestCategory
    {
        public TestCategory()
        {
            Tests = new HashSet<Test>();
        }

        public byte Id { get; set; }
        public string Category { get; set; }

        public virtual ICollection<Test> Tests { get; set; }
    }
}
