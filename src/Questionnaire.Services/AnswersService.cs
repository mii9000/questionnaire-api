using System;
using Questionnaire.Common;
using Questionnaire.Data;
using System.Threading.Tasks;
using Questionnaire.Common.RequestModels;
using Questionnaire.Data.Models;
using System.Collections.Generic;
using System.Linq;
using ServiceStack;

namespace Questionnaire.Services
{
    public interface IAnswersService
    {
        Task<int> SubmitAnswersAsync(QuestionnaireSubmission model);
        Task<string> GetResultsAsync();
    }

    public class AnswersService : IAnswersService
    {
        private readonly IRepository _repository;

        public AnswersService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> SubmitAnswersAsync(QuestionnaireSubmission model)
        {
            var entity = new QuestionnaireSubmit
            {
                Id = model.Id,
                UserId = model.UserId,
                IsCompleted = model.IsCompleted,
                LastUpdated = DateTime.Now,
                QuestionAnswers = model.QuestionAnswers.Select(x => new QuestionnaireQuestionAnswer
                {
                    Id = x.Id,
                    Text = x.Text,
                    Answers = x.Answers
                }).ToList()
            };

            return await _repository.InsertQuestionnaireAsync(entity);
        }

        public async Task<string> GetResultsAsync()
        {
            var result = await _repository.GetResultsAsync();
            return result.ToCsv();
        }
    }
}
