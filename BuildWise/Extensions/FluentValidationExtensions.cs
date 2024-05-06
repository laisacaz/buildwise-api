using FluentValidation;
using FluentValidation.Results;

namespace BuildWise.Extensions
{
    public static class FluentValidationExtensions
    {
        public const string messagePlaceholder = "messagePlaceholder-";
        public static void ThrowExceptionIfNotValid(this ValidationResult validationResult)
        {
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        public static void ThrowExceptionIfNotValid(this ValidationResult validationResult, Exception exception)
        {
            if (!validationResult.IsValid)
            {
                throw exception;
            }
        }

        public static IRuleBuilderOptions<T, TProperty> WithArguments<T, TProperty>(
         this IRuleBuilderOptions<T, TProperty> rule,
         Func<T, string[]> callback
     )
        {
            return rule.Configure(cfg =>
            {
                cfg.MessageBuilder = ctx =>
                {
                    var additionalArgs = callback(ctx.InstanceToValidate);

                    for (var item = 0; item < additionalArgs.Length; item++)
                    {
                        ctx.MessageFormatter.AppendArgument($"{messagePlaceholder}{item}", additionalArgs[item]);
                    }

                    return ctx.GetDefaultMessage();
                };
            });
        }

    }
}
