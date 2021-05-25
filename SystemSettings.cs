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
        }

        public void Reset()
        {
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

        public void Copy(SystemSettings newSettings)
        {
            if (newSettings.name != null)
                name = newSettings.name;

            if (newSettings.location != null)
                location = newSettings.location;
            
            if (newSettings.helpNumber != null)
                helpNumber = newSettings.helpNumber;

            for (int i = 0; i < newSettings.inputs.Length; i++)
            {
                if (newSettings.inputs[i] != null)
                    inputs[i] = newSettings.inputs[i];
            }

            for (int i = 0; i < newSettings.outputs.Length; i++)
            {
                if (newSettings.outputs[i] != null)
                    outputs[i] = newSettings.outputs[i];
            }
        }
    }
}