using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Questionnaire.Data.Models;

namespace Questionnaire.Data
{
    public interface IRepository
    {
        Task<IEnumerable<Answer>> GetAnswersByQuestionIdAsync(int questionId);
        Task<QuestionGroup> GetQuestionGroupsByIdAsync(int id);
        Task<IEnumerable<Question>> GetQuestionsByGroupIdAsync(int groupId);
        Task<IEnumerable<RestrictedAnswer>> GetRestrictedAnswersByQuestionAsync(int questionId);
        Task<int> InsertQuestionnaireAsync(QuestionnaireSubmit model);
        Task<IEnumerable<QuestionnaireResult>> GetResultsAsync();
    }

    public class Repository : BaseRepository, IRepository
    {
        private readonly string _connectionString;

        public Repository(string connectionString) : base(connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<QuestionGroup> GetQuestionGroupsByIdAsync(int id)
        {
            var query = @"SELECT * FROM question_groups WHERE id = @id";
            return await GetData(async connection => await connection.QueryFirstOrDefaultAsync<QuestionGroup>(query, new { id }));
        }

        public async Task<IEnumerable<Answer>> GetAnswersByQuestionIdAsync(int questionId)
        {
            var query = @"SELECT a.id, a.[text]
                        FROM question_answers AS qa
                        INNER JOIN questions AS q ON q.id = qa.question_id
                        INNER JOIN answers AS a ON a.id = qa.answer_id
                        WHERE q.id = @questionId
                        ORDER BY a.[order] ASC";

            return await GetData(async connection => await connection.QueryAsync<Answer>(query, new { questionId }));
        }

        public async Task<IEnumerable<Question>> GetQuestionsByGroupIdAsync(int groupId)
        {
            var query = @"SELECT q.id AS QuestionId, q.[text] AS Text, qt.[text] AS QuestionType 
                        FROM questions AS q
                        INNER JOIN question_types AS qt ON qt.id = q.question_type_id
                        WHERE q.question_group_id = @groupId
                        ORDER BY q.[order] ASC";

            return await GetData(async connection => await connection.QueryAsync<Question>(query, new { groupId }));
        }

        public async Task<IEnumerable<RestrictedAnswer>> GetRestrictedAnswersByQuestionAsync(int questionId)
        {
            var query = @"SELECT a.id
                        FROM restricted_question_answers AS rqa
                        INNER JOIN questions AS q ON q.id = rqa.question_id
                        INNER JOIN answers AS a ON a.id = rqa.answer_id
                        WHERE q.id = @questionId";

            return await GetData(async connection => await connection.QueryAsync<RestrictedAnswer>(query, new { questionId }));
        }


        public async Task<int> InsertQuestionnaireAsync(QuestionnaireSubmit model)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var questionnaireId = 0;

            using (var transaction = await connection.BeginTransactionAsync())
            {
                var insertQuestionnaire = @"INSERT INTO questionnaires (user_id, is_completed, last_updated)
                                            OUTPUT INSERTED.[id]
                                            VALUES (@userId, @isCompleted, @lastUpdated)";

                var insertQuestionnaireQuestion = @"INSERT INTO questionnaire_questions (questionnaire_id, question_group_id, question_id)
                                                    OUTPUT INSERTED.[id]
                                                    VALUES (@questionnaireId, @questionGroupId, @questionId)";


                var insertQuestionnaireAnswers = @"INSERT INTO questionnaire_answers (questionnaire_question_id, answer_id, answer_text)
                                                   OUTPUT INSERTED.[id]
                                                   VALUES (@questionnaireQuestionId, @answerId, @answerText)";


                questionnaireId = await connection.ExecuteScalarAsync<int>(insertQuestionnaire,
                    new { userId = model.UserId, isCompleted = model.IsCompleted, lastUpdated = model.LastUpdated }, transaction);

                foreach (var qa in model.QuestionAnswers)
                {
                    var qustionnaireQuestionId = await connection.ExecuteScalarAsync<int>(insertQuestionnaireQuestion,
                        new { questionnaireId, questionGroupId = model.Id, questionId = qa.Id }, transaction);

                    foreach (var answer in qa.Answers)
                    {
                        await connection.ExecuteAsync(insertQuestionnaireAnswers,
                            new { questionnaireQuestionId = qustionnaireQuestionId, answerId = answer, answerText = default(string) }, transaction);
                    }

                    if (!string.IsNullOrWhiteSpace(qa.Text))
                    {
                        await connection.ExecuteAsync(insertQuestionnaireAnswers,
                            new { questionnaireQuestionId = qustionnaireQuestionId, answerId = default(int?), answerText = qa.Text }, transaction);
                    }
                }

                await transaction.CommitAsync();
            }

            return questionnaireId;
        }


        public async Task<IEnumerable<QuestionnaireResult>> GetResultsAsync()
        {
            var query = @"SELECT QuestionnaireId, [User], Question, Answer FROM (
                        SELECT Q.id AS QuestionnaireId, U.[name] AS [User], QS.[text] AS Question, QA.answer_text AS Answer, QS.[order]
                        FROM questionnaire_questions AS QQ
                        INNER JOIN questionnaire_answers AS QA ON QA.questionnaire_question_id = QQ.id
                        INNER JOIN questionnaires AS Q ON Q.id = QQ.questionnaire_id
                        INNER JOIN questions as QS ON QS.id = QQ.question_id
                        INNER JOIN users as U ON U.id = Q.user_id
                        WHERE QA.answer_text IS NOT NULL
                        GROUP BY Q.id, QS.[text], QA.answer_text, QS.[order], U.[name]
                        UNION 
                        SELECT Q.id AS QuestionnaireId, U.[name] AS [User], QS.[text] AS Question, STRING_AGG(A.[text], ',') AS Answer, QS.[order] 
                        FROM questionnaire_questions AS QQ
                        INNER JOIN questionnaire_answers AS QA ON QA.questionnaire_question_id = QQ.id
                        INNER JOIN questionnaires AS Q ON Q.id = QQ.questionnaire_id
                        INNER JOIN questions as QS ON QS.id = QQ.question_id
                        INNER JOIN users as U ON U.id = Q.user_id
                        INNER JOIN answers as A ON A.id = QA.answer_id
                        GROUP BY Q.id, QS.[text], QA.answer_text, QS.[order], U.[name]
                        ) T ORDER BY T.[order]";

            return await GetData(async connection => await connection.QueryAsync<QuestionnaireResult>(query));
        }
    }
}
