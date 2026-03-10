using FluentResults;
using Microsoft.AspNetCore.Mvc;
using SimpleNotes.Infrastructure;

namespace SimpleNotes.Api.Common
{
    public static class ResultBaseExtensions
    {
        public static ProblemDetails GetFailedProblemDetails(this IResultBase result)
        {
            if (result.IsSuccess)
            {
                throw new InvalidOperationException("Cannot get problem details from successful result");
            }

            ProblemDetails error;
            var firstError = result.Errors.FirstOrDefault();

            if (firstError is ValidationError validationError)
            {
                error = new ValidationProblemDetails(validationError.Errors)
                {
                    Title = validationError.Message,
                    Status = StatusCodes.Status400BadRequest
                };
            }
            else if (firstError is KnownError knownError)
            {
                error = new ProblemDetails
                {
                    Status = knownError.StatusCode,
                    Title = knownError.Message
                };
            }
            else
            {
                error = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal server error",
                    Detail = string.Join(";", result.Errors.Select(e => e.Message))
                };
            }
            return error;
        }

        public static IActionResult GetFailedActionResult(this IResultBase result)
        {
            var error = GetFailedProblemDetails(result);
            return new ObjectResult(error)
            {
                StatusCode = error.Status
            };
        }
    }
}
