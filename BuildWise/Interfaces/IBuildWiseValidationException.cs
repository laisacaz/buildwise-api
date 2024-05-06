using FluentValidation.Results;

namespace BuildWise.Interfaces
{
    public interface IBuildWiseValidationException
    {
        string MessageError { get; }
        string CodeError { get; }
        IEnumerable<ValidationFailure> Errors { get; }
    }
}
