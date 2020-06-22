using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using Serilog;

namespace StriveGateway.API
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RequestResponseLoggingMiddleware
    {
        private readonly ILoggerFactory /*ILogger<RequestResponseLoggingMiddleware>*/ _logger;
        private readonly RequestDelegate _next;
        //private Func _defaultFormatter = (state, exception) => state;


        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
           // _logger = loggerFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var requestBodyStream = new MemoryStream();
            var originalRequestBody = context.Request.Body;

            await context.Request.Body.CopyToAsync(requestBodyStream);
            requestBodyStream.Seek(0, SeekOrigin.Begin);

            var url = UriHelper.GetDisplayUrl(context.Request);
            var requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();
            Log.Information($"REQUEST METHOD: {context.Request.Method}, " +
                $"REQUEST BODY: {requestBodyText}, REQUEST URL: {url}", 
                null);

            requestBodyStream.Seek(0, SeekOrigin.Begin);
            context.Request.Body = requestBodyStream;

            await _next(context);
            context.Request.Body = originalRequestBody;

            /*
            context.Request.EnableBuffering();
            var builder = new StringBuilder();
            var request = await FormatRequest(context.Request);
            builder.Append("Request: ").AppendLine(request);
            builder.AppendLine("Request headers:");

 
            foreach (var header in context.Request.Headers)
            {
                builder.Append(header.Key).Append(':').AppendLine(header.Value);
            }

            _logger.LogInformation(builder.ToString());

            */




            /*
                        //Copy a pointer to the original response body stream
                        var originalBodyStream = context.Response.Body;
                        //Create a new memory stream...
                        using (var responseBody = new MemoryStream())
                        {
                            //...and use that for the temporary response body
                            context.Response.Body = responseBody;

                            //Continue down the Middleware pipeline, eventually returning to this class
                            await _next(context);

                            //Format the response from the server
                            var response = await FormatResponse(context.Response);
                            builder.Append("Response: ").AppendLine(response);
                            builder.AppendLine("Response headers: ");
                            foreach (var header in context.Response.Headers)
                            {
                                builder.Append(header.Key).Append(':').AppendLine(header.Value);
                            }
                            //Save log to chosen datastore
                            _logger.LogInformation(builder.ToString());
                            //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
                            await responseBody.CopyToAsync(originalBodyStream);
                        }
                        */
        }


        private async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;
            //request.EnableRewind();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body = body;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return $"Response {text}";
        }



        //private async Task<string> FormatRequest(HttpRequest request)
        //{
        //    // Leave the body open so the next middleware can read it.
        //    using (var reader = new StreamReader(
        //        request.Body,
        //        Encoding.UTF8,
        //        false,
        //        10000,
        //        leaveOpen: true))
        //    {
        //        var body = await reader.ReadToEndAsync();
        //        // Do some processing with body…
        //        var formattedRequest = $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {body}";
        //        // Reset the request body stream position so the next middleware can read it
        //        request.Body.Position = 0;

        //        return formattedRequest;
        //    }
        //}
        //private async Task<string> FormatResponse(HttpResponse response)
        //{
        //    //We need to read the response stream from the beginning...
        //    response.Body.Seek(0, SeekOrigin.Begin);
        //    //...and copy it into a string
        //    string text = await new StreamReader(response.Body).ReadToEndAsync();
        //    //We need to reset the reader for the response so that the client can read it.
        //    response.Body.Seek(0, SeekOrigin.Begin);
        //    //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
        //    return $"{response.StatusCode}: {text}";
        //}
    }
    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RequestResponseLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
}
