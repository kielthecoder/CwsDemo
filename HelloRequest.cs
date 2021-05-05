using System;
using System.Collections.Generic;
using Crestron.SimplSharp;
using Crestron.SimplSharp.CrestronIO;
using Crestron.SimplSharp.WebScripting;
using Newtonsoft.Json;

namespace CwsDemo
{
    public class HelloRequest : IHttpCwsHandler
    {
        private static string _name = "World";

        public void ProcessRequest(HttpCwsContext context)
        {
            var method = context.Request.HttpMethod;

            switch (method)
            {
                case "GET":
                    context.Response.StatusCode = 200;  // OK
                    context.Response.ContentType = "application/json";

                    var temp = new Dictionary<string, string>();

                    if (context.Request.RouteData.Values.ContainsKey("NAME"))
                    {
                        temp["name"] = context.Request.RouteData.Values["NAME"].ToString();
                        context.Response.Write(JsonConvert.SerializeObject(temp), true);
                    }
                    else
                    {
                        temp["name"] = _name;
                        context.Response.Write(JsonConvert.SerializeObject(temp), true);
                    }
                    break;
                case "PUT":
                    context.Response.StatusCode = 200;  // OK
                    context.Response.ContentType = "application/json";

                    using (var reader = new StreamReader(context.Request.InputStream))
                    {
                        var obj = JsonConvert.DeserializeObject<Dictionary<string, string>>(reader.ReadToEnd());
                        
                        if (obj.ContainsKey("name"))
                        {
                            _name = obj["name"];
                            context.Response.Write("{ \"status\": \"OK\" }", true);
                        }
                    }

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