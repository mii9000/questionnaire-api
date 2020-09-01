using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Questionnaire.Common;

namespace Questionnaire.API.Filters
{
    public class ExceptionResponseFilter : IExceptionFilter
    {
        public ExceptionResponseFilter()
        {
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is NotFoundException exception)
            {
                context.Result = new ObjectResult(exception.Message) { StatusCode = 400 };
                context.ExceptionHandled = true;
            }
        }
    }
}
