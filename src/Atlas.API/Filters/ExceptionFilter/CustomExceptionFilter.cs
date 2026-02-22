using Atlas.API.Filters.ExceptionFilter.Handlers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Atlas.API.Filters.ExceptionFilter
{
    public class CustomExceptionFilter(IEnumerable<ICustomExceptionHandler> handlers)
        : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var handler = handlers.FirstOrDefault(handler => handler.CanHandle(context.Exception));
            if (handler is not null)
            {
                var result = handler.Handle(context.Exception);
                context.Result = result;
                context.ExceptionHandled = true;
            }
        }
    }
}
