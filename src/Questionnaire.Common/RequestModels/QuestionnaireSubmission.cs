using System;
using System.Collections.Generic;

namespace Questionnaire.Common.RequestModels
{
    public class QuestionnaireSubmission
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsCompleted { get; set; }
        public List<QuestionnaireSubmissionQuestionAnswers> QuestionAnswers { get; set; }
    }
}
