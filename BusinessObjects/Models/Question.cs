using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
            QuestionGroupQuestions = new HashSet<QuestionGroupQuestion>();
        }

        public string Id { get; set; }
        public string Content { get; set; }
        public byte? QuestionCategoryId { get; set; }
        public string QuestionCreatorId { get; set; }

        public virtual QuestionCategory QuestionCategory { get; set; }
        public virtual TestCreator QuestionCreator { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<QuestionGroupQuestion> QuestionGroupQuestions { get; set; }
    }
}
