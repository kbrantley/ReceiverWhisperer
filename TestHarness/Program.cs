using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReceiverWhisperer.Core;
using ReceiverWhisperer.Onkyo;

namespace TestHarness {
    class Program {

        static void Main(string[] args) {
            Receiver r = new OnkyoReceiver("TX-NR515", "10.254.254.98", 60128);
            r.Connect();
            Console.WriteLine("Connected.");
            r.VolumeControl.SubscribeVolumeChanges(VolumeChanged);
            r.VolumeControl.SubscribeMuteChanges(MuteChanged);
            r.PowerManagement.SubscribeDisplayBrightnessChanges(DisplayBrightnessChanged);
            r.PowerManagement.SubscribePowerChanges(PowerChanged);
            r.PowerManagement.SubscribeSleepChanges(SleepChanged);
            r.Inputs.SubscribeInputChanges(InputChanged);
            Console.ReadLine();
            r.Disconnect();
        }

        private static void VolumeChanged(Object sender, ReceiverEventArgs e) {
            Console.WriteLine("Volume: " + ((VolumeEventArgs)e).Volume);
        }

        private static void MuteChanged(Object sender, ReceiverEventArgs e) {
            if (((MuteEventArgs)e).Muted)
                Console.WriteLine("Muted");
            else
                Console.WriteLine("Not Muted");
        }

        private static void DisplayBrightnessChanged(Object sender, ReceiverEventArgs e) {
            Console.WriteLine("Display Brightness: " + ((DisplayBrightnessEventArgs)e).Brightness);
        }

        private static void PowerChanged(Object sender, ReceiverEventArgs e) {
            if (((PowerEventArgs)e).Powered)
                Console.WriteLine("Power On");
            else
                Console.WriteLine("Power Off");
        }

        private static void SleepChanged(Object sender, ReceiverEventArgs e) {
            if (((SleepEventArgs)e).SleepTimer == 0)
                Console.WriteLine("Sleep off");
            else
                Console.WriteLine("Sleep Timer: " + ((SleepEventArgs)e).SleepTimer + " minutes");
        }

        private static void InputChanged(Object sender, ReceiverEventArgs e) {
            Console.WriteLine("Active Input: " + ((InputEventArgs)e).Input.Name);
        }
    }
}
