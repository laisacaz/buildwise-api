namespace BuildWise.ExceptionMiddleware
{
    public class ValidationError
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
        public string PropertyName { get; set; }
        public object CustomState { get; set; }
        public object AttemptedValue { get; set; }
    }
}
