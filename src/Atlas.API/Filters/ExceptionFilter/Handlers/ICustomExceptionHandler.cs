using Microsoft.AspNetCore.Mvc;

namespace Atlas.API.Filters.ExceptionFilter.Handlers
{
    public interface ICustomExceptionHandler
    {
        bool CanHandle(Exception exception);
        IActionResult Handle(Exception exception);
    }
}
