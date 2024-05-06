namespace BuildWise.ExceptionMiddleware
{
    public class DefaultExceptionResponse
    {
        public string Message { get; set; }
        public string MessageCode { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
        public IEnumerable<ValidationError> ValidationErrors { get; set; }
        public System.Exception Exception { get; set; }
    }
}
