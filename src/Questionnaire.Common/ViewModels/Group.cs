using System;
using System.Collections.Generic;

namespace Questionnaire.Common.ViewModels
{
    public class Group
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<Question> Questions { get; set; }
    }
}
