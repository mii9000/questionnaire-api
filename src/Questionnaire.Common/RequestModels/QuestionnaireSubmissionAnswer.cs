using System;
using System.Collections.Generic;

namespace Questionnaire.Common.RequestModels
{
    public class QuestionnaireSubmissionQuestionAnswers
    {
        public int Id { get; set; }
        public List<int> Answers { get; set; }
        public string Text { get; set; }
    }
}
