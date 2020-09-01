using NUnit.Framework;
using Questionnaire.Services;
using Questionnaire.Data;
using Moq;
using Questionnaire.Data.Models;
using System.Collections.Generic;
using Questionnaire.Common;
using System.Threading.Tasks;
using Questionnaire.Common.ViewModels;
using DeepEqual.Syntax;

namespace Questionnaire.Tests
{
    public class QuestionsServiceTests
    {
        private IQuestionsService _sut;
        private Mock<IRepository> _repository;


        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IRepository>();
            _sut = new QuestionsService(_repository.Object);
        }


        [Test]
        public void When_QuestionGroup_NotFound_Should_Exception()
        {
            _repository
                .Setup(x => x.GetQuestionGroupsByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(default(QuestionGroup));

            Assert.ThrowsAsync<NotFoundException>(() => _sut.GetAllQuestionsByIdAsync(It.IsAny<int>()));
        }


        [Test]
        public async Task When_Requesting_All_Questions_Should_Success()
        {
            _repository
                .Setup(x => x.GetQuestionGroupsByIdAsync(1))
                .ReturnsAsync(new QuestionGroup { Id = 1, Text = "Personal Information" });

            _repository
                .Setup(x => x.GetQuestionsByGroupIdAsync(1))
                .ReturnsAsync(new List<Data.Models.Question> { new Data.Models.Question { QuestionId = 1, QuestionType = "input_text", Text = "First Name" } });

            _repository
                .Setup(x => x.GetAnswersByQuestionIdAsync(1))
                .ReturnsAsync(new List<Data.Models.Answer>());

            _repository
                .Setup(x => x.GetRestrictedAnswersByQuestionAsync(1))
                .ReturnsAsync(new List<RestrictedAnswer>());

            var actual = await _sut.GetAllQuestionsByIdAsync(1);

            var expected = new Group
            {
                Id = 1,
                Text = "Personal Information",
                Questions = new List<Common.ViewModels.Question>
                {
                    new Common.ViewModels.Question
                    {
                        Id = 1,
                        Text = "First Name",
                        Type = "input_text",
                        Answers = new List<Common.ViewModels.Answer>(),
                        Restrictions = new List<int>()
                    }
                }
            };

            expected.ShouldDeepEqual(actual);
        }


        [Test]
        public async Task When_No_Questions_In_Group_Should_Success()
        {
            _repository
                .Setup(x => x.GetQuestionGroupsByIdAsync(1))
                .ReturnsAsync(new QuestionGroup { Id = 1, Text = "Personal Information" });

            _repository
                .Setup(x => x.GetQuestionsByGroupIdAsync(1))
                .ReturnsAsync(new List<Data.Models.Question>());

            var actual = await _sut.GetAllQuestionsByIdAsync(1);

            var expected = new Group
            {
                Id = 1,
                Text = "Personal Information",
                Questions = new List<Common.ViewModels.Question>()
            };

            expected.ShouldDeepEqual(actual);
        }
    }
}