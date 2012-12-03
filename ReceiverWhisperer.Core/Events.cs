using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReceiverWhisperer.Core {
    public delegate void ReceiverEventHandler(Object sender, ReceiverEventArgs e);

    public class ReceiverEventArgs : EventArgs { }

    #region Power
    public class PowerEventArgs : ReceiverEventArgs {
        public bool Powered { get; private set; }
        public PowerEventArgs(bool powered) {
            Powered = powered;
        }
    }

    public class DisplayBrightnessEventArgs : ReceiverEventArgs {
        public int Brightness { get; private set; }
        public DisplayBrightnessEventArgs(int brightness) {
            Brightness = brightness;
        }
    }

    public class SleepEventArgs : ReceiverEventArgs {
        public int SleepTimer { get; private set; }
        public SleepEventArgs(int sleepTimer) {
            SleepTimer = sleepTimer;
        }
    }
    #endregion

    #region Volume / Mute
    public class VolumeEventArgs : ReceiverEventArgs {
        public int Volume { get; private set; }
        public double VolumePercentage { get; private set; }
        public VolumeEventArgs(int volume, double volumePercentage) {
            Volume = volume;
            VolumePercentage = volumePercentage;
        }
    }

    public class MuteEventArgs : ReceiverEventArgs {
        public bool Muted { get; private set; }
        public MuteEventArgs(bool muted) {
            Muted = muted;
        }
    }
    #endregion

}
