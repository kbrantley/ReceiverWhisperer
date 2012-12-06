using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReceiverWhisperer.Core {

    public delegate void ReceiverEventHandler(Object sender, ReceiverEventArgs e);
    public class ReceiverEventArgs : EventArgs { }

    #region Power
    public delegate void PowerEventHandler(Object sender, PowerEventArgs e);
    public class PowerEventArgs : ReceiverEventArgs {
        public bool Powered { get; private set; }
        public PowerEventArgs(bool powered) {
            Powered = powered;
        }
    }

    public delegate void DisplayBrightnessEventHandler(Object sender, DisplayBrightnessEventArgs e);
    public class DisplayBrightnessEventArgs : ReceiverEventArgs {
        public int Brightness { get; private set; }
        public DisplayBrightnessEventArgs(int brightness) {
            Brightness = brightness;
        }
    }

    public delegate void SleepEventHandler(Object sender, SleepEventArgs e);
    public class SleepEventArgs : ReceiverEventArgs {
        public int SleepTimer { get; private set; }
        public SleepEventArgs(int sleepTimer) {
            SleepTimer = sleepTimer;
        }
    }
    #endregion

    #region Volume / Mute
    public delegate void VolumeEventHandler(Object sender, VolumeEventArgs e);
    public class VolumeEventArgs : ReceiverEventArgs {
        public int Volume { get; private set; }
        public double VolumePercentage { get; private set; }
        public VolumeEventArgs(int volume, double volumePercentage) {
            Volume = volume;
            VolumePercentage = volumePercentage;
        }
    }

    public delegate void MuteEventHandler(Object sender, MuteEventArgs e);
    public class MuteEventArgs : ReceiverEventArgs {
        public bool Muted { get; private set; }
        public MuteEventArgs(bool muted) {
            Muted = muted;
        }
    }
    #endregion

    #region Inputs
    public delegate void InputEventHandler(Object sender, InputEventArgs e);
    public class InputEventArgs : ReceiverEventArgs {
        public Input Input { get; private set; }
        public InputEventArgs(Input input) {
            Input = input;
        }
    }
    #endregion

}
