using System;
namespace Questionnaire.Data.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public string QuestionType { get; set; }
    }
}
