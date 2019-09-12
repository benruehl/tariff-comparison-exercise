using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TariffComparison.Web.DTOs;

namespace TariffComparison.Web.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IOptions<MvcJsonOptions> jsonOptions)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e, jsonOptions);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception e, IOptions<MvcJsonOptions> jsonOptions)
        {
            string resultBody = JsonConvert.SerializeObject(new ErrorDTO(e.Message), jsonOptions.Value.SerializerSettings);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(resultBody);
        }
    }
}
