namespace ChefsKiss.Web.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;

    using ChefsKiss.Web.Models;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class ErrorController : Controller
    {
        private readonly Dictionary<int, string> MessageByStatusCode = new()
        {
            // No code
            [0] = "Unknown error",

            // Client errors
            [400] = "Bad Request",
            [401] = "Unauthorized",
            [402] = "Payment Required",
            [403] = "Forbidden",
            [404] = "Not Found",
            [405] = "Method Not Allowed",
            [406] = "Not Acceptable",
            [407] = "Proxy Authentication Required",
            [408] = "Request Timeout",
            [409] = "Conflict",
            [410] = "Gone",
            [411] = "Length Required",
            [412] = "Precondition Failed",
            [413] = "Payload Too Large",
            [414] = "Request-URI Too Long",
            [415] = "Unsupported Media Type",
            [416] = "Requested Range Not Satisfiable",
            [417] = "Expectation Failed",
            [418] = "I'm a teapot",
            [421] = "Misdirected Request",
            [422] = "Unprocessable Entity",
            [423] = "Locked",
            [424] = "Failed Dependency",
            [426] = "Upgrade Required",
            [428] = "Precondition Required",
            [429] = "Too Many Requests",
            [431] = "Request Header Fields Too Large",
            [444] = "Connection Closed Without Response",
            [451] = "Unavailable For Legal Reasons",
            [499] = "Client Closed Request",

            // Server errors
            [500] = "Internal Server Error",
            [501] = "Not Implemented",
            [502] = "Bad Gateway",
            [503] = "Service Unavailable",
            [504] = "Gateway Timeout",
            [505] = "HTTP Version Not Supported",
            [506] = "Variant Also Negotiates",
            [507] = "Insufficient Storage",
            [508] = "Loop Detected",
            [510] = "Not Extended",
            [511] = "Network Authentication Required",
            [599] = "Network Connect Timeout Error",
        };

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index(int statusCode = StatusCodes.Status500InternalServerError)
        {
            var error = new ErrorViewModel
            {
                StatusCode = statusCode,
                RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier,
            };

            if (this.MessageByStatusCode.ContainsKey(statusCode))
            {
                error.Message = this.MessageByStatusCode[statusCode];
            }

            return this.View(error);
        }
    }
}
