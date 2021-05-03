using System;
using Crestron.SimplSharp;
using Crestron.SimplSharp.CrestronIO;
using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.CrestronThread;
using Crestron.SimplSharp.WebScripting;

namespace CwsDemo
{
    public class ControlSystem : CrestronControlSystem
    {
        private HttpCwsServer _api;

        public ControlSystem()
            : base()
        {
            try
            {
                Thread.MaxNumberOfUserThreads = 20;

                CrestronEnvironment.ProgramStatusEventHandler += ProgramStatusHandler;
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in the constructor: {0}", e.Message);
            }
        }

        public override void InitializeSystem()
        {
            try
            {
                _api = new HttpCwsServer("/api");

                var hello = new HttpCwsRoute("hello/");
                hello.RouteHandler = new HelloRequest();
                _api.AddRoute(hello);

                _api.Register();
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in InitializeSystem: {0}", e.Message);
            }
        }

        private void ProgramStatusHandler(eProgramStatusEventType status)
        {
            if (status == eProgramStatusEventType.Stopping)
            {
                if (_api != null)
                {
                    _api.Unregister();
                    _api.Dispose();
                }
            }
        }
    }
}