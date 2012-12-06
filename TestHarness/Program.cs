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

        private static void VolumeChanged(Object sender, VolumeEventArgs e) {
            Console.WriteLine("Volume: " + e.Volume);
        }

        private static void MuteChanged(Object sender, MuteEventArgs e) {
            if (e.Muted)
                Console.WriteLine("Muted");
            else
                Console.WriteLine("Not Muted");
        }

        private static void DisplayBrightnessChanged(Object sender, DisplayBrightnessEventArgs e) {
            Console.WriteLine("Display Brightness: " + e.Brightness);
        }

        private static void PowerChanged(Object sender, PowerEventArgs e) {
            if (e.Powered)
                Console.WriteLine("Power On");
            else
                Console.WriteLine("Power Off");
        }

        private static void SleepChanged(Object sender, SleepEventArgs e) {
            if (e.SleepTimer == 0)
                Console.WriteLine("Sleep off");
            else
                Console.WriteLine("Sleep Timer: " + e.SleepTimer + " minutes");
        }

        private static void InputChanged(Object sender, InputEventArgs e) {
            Console.WriteLine("Active Input: " + e.Input.Name);
        }
    }
}
