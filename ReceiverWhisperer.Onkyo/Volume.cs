using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReceiverWhisperer.Core;

namespace ReceiverWhisperer.Onkyo {
    class Volume : VolumeControl {
        private OnkyoReceiver _receiver;
        private int _currentVolume = 0;
        private bool _isMuted = false;

        int VolumeControl.Volume { get { return _currentVolume; } set { SetVolume(value); } }
        public double VolumePercentage { get { return (double)_currentVolume / (double)MaximumVolume; } set { SetVolume(value); } }
        public int MinimumVolume { get; private set; }
        public int MaximumVolume { get; private set; }
        public bool Muted { get { return _isMuted; } set { SetMuted(value); } }

        public Volume(OnkyoReceiver receiver) {
            this._receiver = receiver;
            MinimumVolume = 0;
            MaximumVolume = 0x50;
            _receiver._ipp.Subscribe("MVL", VolumeChanged);
            _receiver._ipp.Subscribe("AMT", MuteChanged);
            new OutboundPacket(_receiver, "MVLQSTN");
            new OutboundPacket(_receiver, "AMTQSTN");
        }

        private void SetVolume(int volume) {
            if (volume < MinimumVolume)
                new OutboundPacket(_receiver, "MVL" + MinimumVolume.ToString("X2"));
            else if (volume > MaximumVolume)
                new OutboundPacket(_receiver, "MVL" + MaximumVolume.ToString("X2"));
            else
                new OutboundPacket(_receiver, "MVL" + volume.ToString("X2"));
        }

        private void SetVolume(double volume) {
            // setting the volume outside of the min/max is handled in the int-based SetVolume()
            // in short, we don't care if we pass 9001 here, it will be handled properly
            SetVolume((int)Math.Floor((double)MaximumVolume * volume));
        }

        private void SetMuted(bool muted) {
            if (muted)
                new OutboundPacket(_receiver, "AMT01");
            else
                new OutboundPacket(_receiver, "AMT00");
        }

        public void StepUp() {
            new OutboundPacket(_receiver, "MVLUP");
        }

        public void StepDown() {
            new OutboundPacket(_receiver, "MVLDOWN");
        }

        void VolumeChanged(Object source, MessageEventArgs e) {
            try {
                _currentVolume = Int32.Parse(e.Message.Substring(3), System.Globalization.NumberStyles.HexNumber);
            } catch (Exception) { } // parse error, assume value is 'N/A' and proceed
        }

        void MuteChanged(Object source, MessageEventArgs e) {
            if (e.Message == "AMT00")
                _isMuted = false;
            else
                _isMuted = true;
        }

    }
}
