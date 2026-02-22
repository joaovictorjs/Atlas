using Atlas.API.Filters.ExceptionFilter.Responses;
using Atlas.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Atlas.API.Filters.ExceptionFilter.Handlers
{
    public class DomainExceptionHandler : ICustomExceptionHandler
    {
        public bool CanHandle(Exception exception)
        {
            return exception is DomainException;
        }

        public IActionResult Handle(Exception exception)
        {
            var problem = new ErrorResponse(exception.Message);
            var result = new ObjectResult(problem)
            {
                StatusCode = StatusCodes.Status422UnprocessableEntity,
            };
            return result;
        }
    }
}
