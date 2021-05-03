using System;
using Crestron.SimplSharp;
using Crestron.SimplSharp.WebScripting;

namespace CwsDemo
{
    public class HelloRequest : IHttpCwsHandler
    {
        public void ProcessRequest(HttpCwsContext context)
        {
            var method = context.Request.HttpMethod;

            switch (method)
            {
                case "GET":
                    context.Response.StatusCode = 200;  // OK
                    context.Response.Write("Hello World!", false);
                    context.Response.End();
                    break;
                default:
                    context.Response.StatusCode = 405;  // Method Not Allowed
                    context.Response.End();
                    break;
            }
        }
    }
}