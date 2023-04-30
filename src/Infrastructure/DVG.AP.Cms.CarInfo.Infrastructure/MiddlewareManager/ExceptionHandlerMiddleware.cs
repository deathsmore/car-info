using System.Diagnostics;
using System.Net;
using DVG.AP.Cms.CarInfo.Application.Contracts.Constant;
using DVG.AutoPortal.Core.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DVG.AP.Cms.CarInfo.Infrastructure.MiddlewareManager
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public readonly ILogger _logger;
        private IWebHostEnvironment _environment;
        private readonly Stopwatch _stopwatch;

        public ExceptionHandlerMiddleware(RequestDelegate next, IWebHostEnvironment env,
            ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger("HttpContext");
            _environment = env;
            _stopwatch = new Stopwatch();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
                _logger.LogError(new EventId(e.HResult),
                    e,
                    e.Message);
                await ConvertException(context, e);
            }
        }

        private Task ConvertException(HttpContext context, Exception exception)
        {
            var httpStatusCode = HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            var result = string.Empty;
            var activeId = Activity.Current?.Id ?? context.TraceIdentifier;


            switch (exception)
            {
                case ValidationException validationException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(new
                        {errors = validationException.Errors, trace_id = activeId});

                    break;
                case BadRequestException badRequestException:
                    httpStatusCode = HttpStatusCode.BadRequest;

                    break;
                case NotFoundException notFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    break;
                case ConflictException conflictException:
                    httpStatusCode = HttpStatusCode.Conflict;
                    break;
                case ForbidException forbidException:
                    httpStatusCode = HttpStatusCode.Forbidden;
                    break;
                // ReSharper disable once PatternAlwaysOfType
                case Exception ex:

                    result = JsonConvert.SerializeObject(new
                        {errors = new List<string> {ExceptionMessages.ExceptionGlobalMessages}, trace_id = activeId});


                    if (!_environment.IsProduction())
                    {
                        result = JsonConvert.SerializeObject(new
                            {errors = exception, trace_id = activeId});
                    }


                    httpStatusCode = HttpStatusCode.InternalServerError;
                    break;
            }


            context.Response.StatusCode = (int) httpStatusCode;
            if (!string.IsNullOrEmpty(result))
                return context.Response.WriteAsync(result);

            var listError = new List<string>()
            {
                exception.Message
            };
            if (!string.IsNullOrEmpty(exception.InnerException?.Message))
            {
                listError.Add(exception.InnerException?.Message);
            }


            result = JsonConvert.SerializeObject(new {errors = listError, trace_id = activeId});

            return context.Response.WriteAsync(result);
        }
    }
}