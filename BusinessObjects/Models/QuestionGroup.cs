using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class QuestionGroup
    {
        public QuestionGroup()
        {
            QuestionGroupQuestions = new HashSet<QuestionGroupQuestion>();
            TestQuestionGroups = new HashSet<TestQuestionGroup>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string QuestionGroupCreatorId { get; set; }

        public virtual TestCreator QuestionGroupCreator { get; set; }
        public virtual ICollection<QuestionGroupQuestion> QuestionGroupQuestions { get; set; }
        public virtual ICollection<TestQuestionGroup> TestQuestionGroups { get; set; }
    }
}
