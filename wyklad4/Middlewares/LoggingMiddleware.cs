using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wyklad4.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            //Our code
            httpContext.Request.EnableBuffering();
            if (httpContext.Request != null)
            {
                string metod = httpContext.Request.Method;
                string path = httpContext.Request.Path;
                
                string body = "";
                using(StreamReader read = new StreamReader(httpContext.Request.Body,Encoding.UTF8, true, 1024, true))
                {
                    body = await read.ReadToEndAsync();
                }
                string query = httpContext.Request?.QueryString.ToString();
                using (StreamWriter writer =new StreamWriter(Path.Combine("requestLog.txt"), true)){
                    writer.WriteLine($"{metod} | {path} | {body} | {query}");
                }
            }
            




            await _next(httpContext);
        }
    }
}
