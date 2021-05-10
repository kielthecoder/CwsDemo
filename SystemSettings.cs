using System;

namespace CwsDemo
{
    public class SystemSettings
    {
        public const int MaxInputs = 8;
        public const int MaxOutputs = 8;

        public string RoomName { get; set; }
        public string Location { get; set; }
        public string HelpNumber { get; set; }

        public string[] InputName { get; set; }
        public string[] OutputName { get; set; }

        public SystemSettings()
        {
            InputName = new string[MaxInputs];
            OutputName = new string[MaxOutputs];

            RoomName = "My Room";
            Location = "My Office";
            HelpNumber = "5555";

            for (int i = 0; i < MaxInputs; i++)
            {
                InputName[i] = String.Format("Input {0}", i + 1);
            }

            for (int i = 0; i < MaxOutputs; i++)
            {
                OutputName[i] = String.Format("Output {0}", i + 1);
            }
        }
    }
}