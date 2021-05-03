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
                    context.Response.Write("Hello World!", true);
                    break;
                default:
                    context.Response.StatusCode = 501;  // Not implemented
                    context.Response.Write(
                        String.Format("{0} method not implemented!", method), true);
                    break;
            }
        }
    }
}