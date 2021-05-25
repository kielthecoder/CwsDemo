using System;
using System.Collections.Generic;
using Crestron.SimplSharp;
using Crestron.SimplSharp.CrestronIO;
using Crestron.SimplSharp.WebScripting;
using Newtonsoft.Json;

namespace CwsDemo
{
    public class RoomRequest : IHttpCwsHandler
    {
        private SystemSettings _settings;

        public RoomRequest()
        {
            _settings = new SystemSettings();
            _settings.Reset();
        }

        public void ProcessRequest(HttpCwsContext context)
        {
            var method = context.Request.HttpMethod;

            switch (method)
            {
                case "GET":
                    context.Response.StatusCode = 200;  // OK
                    context.Response.ContentType = "application/json";

                    if (context.Request.RouteData.Values.ContainsKey("PROPERTY"))
                    {
                        var prop = context.Request.RouteData.Values["PROPERTY"];
                        context.Response.Write(GetPropertyJSON(prop.ToString()), true);
                    }
                    else
                    {
                        try
                        {
                            context.Response.Write(JsonConvert.SerializeObject(_settings), true);
                        }
                        catch (Exception e)
                        {
                            ErrorLog.Error("Exception in RoomRequest.ProcessRequest: {0}",
                                e.Message);
                        }
                    }

                    break;
                case "PUT":
                    context.Response.StatusCode = 200;  // OK
                    context.Response.ContentType = "application/json";

                    SystemSettings newSettings;

                    try
                    {
                        using (var reader = new StreamReader(context.Request.InputStream))
                        {
                            newSettings = JsonConvert.DeserializeObject<SystemSettings>
                                (reader.ReadToEnd());

                            CrestronConsole.PrintLine("Inputs: {0}", newSettings.inputs.Length);
                            CrestronConsole.PrintLine("Outputs: {0}", newSettings.outputs.Length);
                        }

                        _settings.Copy(newSettings);
                    }
                    catch (Exception e)
                    {
                        ErrorLog.Error("Exception in RoomRequest.ProcessRequest: {0}",
                            e.Message);
                    }

                    context.Response.Write("{ \"status\": \"OK\" }", true);
                    break;
                default:
                    context.Response.StatusCode = 501;  // Not implemented
                    context.Response.Write(
                        String.Format("{0} method not implemented!", method),
                        true);
                    break;
            }
        }

        public string GetPropertyJSON(string propName)
        {
            var obj = new Dictionary<string, string>();

            switch (propName)
            {
                case "name":
                    obj["value"] = _settings.name;
                    break;
                case "location":
                    obj["value"] = _settings.location;
                    break;
                case "helpNumber":
                    obj["value"] = _settings.helpNumber;
                    break;
            }

            return JsonConvert.SerializeObject(obj);
        }
    }
}