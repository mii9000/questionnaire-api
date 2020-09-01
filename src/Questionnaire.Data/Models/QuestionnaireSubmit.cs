using System;
using System.Collections.Generic;

namespace Questionnaire.Data.Models
{
    public class QuestionnaireSubmit
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime LastUpdated { get; set; }
        public List<QuestionnaireQuestionAnswer> QuestionAnswers { get; set; }
    }

    public class QuestionnaireQuestionAnswer
    {
        public int Id { get; set; }
        public List<int> Answers { get; set; }
        public string Text { get; set; }
    }
}
