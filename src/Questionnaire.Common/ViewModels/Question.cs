using System;
using System.Collections.Generic;

namespace Questionnaire.Common.ViewModels
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public List<Answer> Answers { get; set; }
        public List<int> Restrictions { get; set; }
    }
}
