using BuildWise.Interfaces;
using FluentValidation.Results;

namespace BuildWise.ExceptionMiddleware
{
    public class BuildWiseValidationException : Exception, IBuildWiseValidationException
    {
        public BuildWiseValidationException(IEnumerable<ValidationFailure> validationErrors)
        {
            Errors = validationErrors;
        }

        public BuildWiseValidationException(ValidationFailure validationError)
        {
            Errors = new List<ValidationFailure> { validationError };
        }

        public string CodeError { get => "ME0400"; }
        public string MessageError { get => "ME0400"; }
        public IEnumerable<ValidationFailure> Errors { get; }
    }
}
