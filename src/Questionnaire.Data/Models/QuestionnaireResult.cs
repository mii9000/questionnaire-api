using System;
namespace Questionnaire.Data.Models
{
    public class QuestionnaireResult
    {
        public int QuestionnaireId { get; set; }
        public string User { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
