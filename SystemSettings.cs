using System;

namespace CwsDemo
{
    public class SystemSettings
    {
        public const int MaxInputs = 8;
        public const int MaxOutputs = 8;

        public string name { get; set; }
        public string location { get; set; }
        public string helpNumber { get; set; }

        public string[] inputs { get; set; }
        public string[] outputs { get; set; }

        public SystemSettings()
        {
            inputs = new string[MaxInputs];
            outputs = new string[MaxOutputs];

            name = "My Room";
            location = "My Office";
            helpNumber = "5555";

            for (int i = 0; i < MaxInputs; i++)
            {
                inputs[i] = String.Format("Input {0}", i + 1);
            }

            for (int i = 0; i < MaxOutputs; i++)
            {
                outputs[i] = String.Format("Output {0}", i + 1);
            }
        }
    }
}