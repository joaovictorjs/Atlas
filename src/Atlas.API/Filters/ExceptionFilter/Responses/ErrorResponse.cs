namespace Atlas.API.Filters.ExceptionFilter.Responses
{
    public record ErrorResponse(string[] Errors)
    {
        public ErrorResponse(string error)
            : this([error]) { }
    }
}
