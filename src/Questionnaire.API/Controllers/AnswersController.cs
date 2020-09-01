using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Questionnaire.Services;
using Questionnaire.Common.ViewModels;
using Questionnaire.Common.RequestModels;
using System.Text;
using System.IO;

namespace Questionnaire.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AnswersController : ControllerBase
    {
        private readonly IAnswersService _answersService;

        public AnswersController(IAnswersService answersService)
        {
            _answersService = answersService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] QuestionnaireSubmission request)
            => await _answersService.SubmitAnswersAsync(request);

        [HttpGet("download")]
        public async Task<IActionResult> Get()
        {
            var result = await _answersService.GetResultsAsync();
            var stream = new MemoryStream(Encoding.ASCII.GetBytes(result));
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "text/csv");
        }
    }
}
