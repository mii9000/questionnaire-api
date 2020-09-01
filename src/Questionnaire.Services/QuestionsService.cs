using System;
using Questionnaire.Data;
using System.Threading.Tasks;
using System.Linq;
using Questionnaire.Common.ViewModels;
using System.Collections.Generic;
using Question = Questionnaire.Common.ViewModels.Question;
using Questionnaire.Common;

namespace Questionnaire.Services
{
    public interface IQuestionsService
    {
        Task<Group> GetAllQuestionsByIdAsync(int groupId);
    }

    public class QuestionsService : IQuestionsService
    {
        private readonly IRepository _repository;

        public QuestionsService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Group> GetAllQuestionsByIdAsync(int groupId)
        {
            var group = await _repository.GetQuestionGroupsByIdAsync(groupId);
            if (group == null) throw new NotFoundException($"Could not find requested question with id '{groupId}'");

            var questions = await _repository.GetQuestionsByGroupIdAsync(groupId);

            var response = new Group
            {
                Id = group.Id,
                Text = group.Text,
                Questions = new List<Question>()
            };

            foreach (var question in questions)
            {
                var answers = (await _repository.GetAnswersByQuestionIdAsync(question.QuestionId)).ToList();
                var restrictions = (await _repository.GetRestrictedAnswersByQuestionAsync(question.QuestionId)).Select(q => q.Id).ToList();
                response.Questions.Add(new Question
                {
                    Id = question.QuestionId,
                    Text = question.Text,
                    Type = question.QuestionType,
                    Answers = answers.Select(a => new Answer { Id = a.Id, Text = a.Text }).ToList(),
                    Restrictions = restrictions
                });
            }

            return response;
        }
    }
}
