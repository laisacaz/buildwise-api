using BuildWise.Extensions;
using BuildWise.Interfaces;
using BuildWise.Pages;
using BuildWise.Provider;
using FluentValidation;
using Newtonsoft.Json;
using System.Net;

namespace BuildWise.ExceptionMiddleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException exception)
            {
                await HandleValidationExceptionAsync(context, exception);
            }
            catch (Exception exception) when (exception is IBuildWiseException buildwiseException)
            {
                await HandleBuildWiseExceptionAsync(context, buildwiseException);
            }
            catch (Exception exception) when (exception is IBuildWiseValidationException buildwiseValidationException)
            {
                await HandleBuildWiseValidationExceptionAsync(context, buildwiseValidationException);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }
        private Task HandleExceptionAsync(
            HttpContext context,
            Exception exception)
        {         

            DefaultExceptionResponse content = new()
            {
                Message = "ME0500",
                MessageCode = "ME0500",
                Exception = exception
            };

            return DefaultResponse(
                context,
                httpCode: HttpStatusCode.InternalServerError,
                content);
        }
        private static Task HandleValidationExceptionAsync(
            HttpContext context,
            ValidationException exception)
        {
            IEnumerable<ValidationError> errors = MapFluentValidationErrors(exception.Errors);

            DefaultExceptionResponse content = new()
            {
                Message = "ME0400",
                MessageCode = "ME0400",
                ValidationErrors = errors
            };

            return DefaultResponse(
                context,
                HttpStatusCode.BadRequest,
                content);
        }

        private Task HandleBuildWiseExceptionAsync(
            HttpContext context,
            IBuildWiseException exception)
        {
            DefaultExceptionResponse content = new()
            {
                Message = exception.MessageError,
                MessageCode = exception.CodeError,
                Exception = (Exception)exception
            };

            return DefaultResponse(
                context,
                httpCode: exception.StatusHttp,
                content);
        }

        private static Task HandleBuildWiseValidationExceptionAsync(
            HttpContext context,
            IBuildWiseValidationException exception)
        {
            IEnumerable<ValidationError> errors = MapFluentValidationErrors(exception.Errors);

            DefaultExceptionResponse content = new()
            {
                MessageCode = exception.CodeError,
                Message = exception.MessageError,
                ValidationErrors = errors
            };

            return DefaultResponse(
                context,
                httpCode: HttpStatusCode.BadRequest,
                content);
        }
        private static IEnumerable<ValidationError> MapFluentValidationErrors(
            IEnumerable<FluentValidation.Results.ValidationFailure> errors)
        {
            return errors.Select(x =>
            {
                Dictionary<string, object> parameters = null;

                if (x.FormattedMessagePlaceholderValues?.Count > 0)
                {
                    parameters = x.FormattedMessagePlaceholderValues
                        .Where(x => x.Key.Contains(FluentValidationExtensions.messagePlaceholder))
                        .ToDictionary(y =>
                            y.Key.Replace(FluentValidationExtensions.messagePlaceholder, string.Empty),
                            y => y.Value
                        );
                }

                return new ValidationError
                {
                    PropertyName = x.PropertyName,
                    ErrorCode = x.ErrorCode,
                    Message = x.ErrorMessage,
                    CustomState = x.CustomState,
                    AttemptedValue = x.AttemptedValue,
                    Parameters = parameters
                };
            });
        }

        private static Task DefaultResponse(
           HttpContext context,
           HttpStatusCode httpCode,
           DefaultExceptionResponse content)
        {
            JsonSerializerSettings jsonConfig = JsonProvider.GetNewtonSoft();

            string result = JsonConvert.SerializeObject(content, jsonConfig);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpCode;

            return context.Response.WriteAsync(result);
        }
    }
}
