using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReceiverWhisperer.Core;

namespace ReceiverWhisperer.Onkyo {
    class Volume : VolumeControl {
        /* Internal use only */
        private OnkyoReceiver _receiver;
        private int _currentVolume = 0;
        private bool _isMuted = false;

        /* Interface defined fields */
        int VolumeControl.Volume { get { return _currentVolume; } set { SetVolume(value); } }
        public double VolumePercentage { get { return (double)_currentVolume / (double)MaximumVolume; } set { SetVolume(value); } }
        public int MinimumVolume { get; private set; }
        public int MaximumVolume { get; private set; }
        public bool Muted { get { return _isMuted; } set { SetMuted(value); } }

        /* External facing events */
        private event ReceiverEventHandler OnVolumeChanged;
        private event ReceiverEventHandler OnMuteChanged;

        public Volume(OnkyoReceiver receiver) {
            this._receiver = receiver;
            MinimumVolume = 0;
            MaximumVolume = 0x50;
            _receiver._ipp.Subscribe("MVL", VolumeChanged);
            _receiver._ipp.Subscribe("AMT", MuteChanged);
            _receiver._ipp.Subscribe("OnConnect", OnConnect);
        }

        public void OnConnect(Object sender, EventArgs e) {
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
                int old = _currentVolume;
                _currentVolume = Int32.Parse(e.Message.Substring(3), System.Globalization.NumberStyles.HexNumber);
                if (old != _currentVolume) 
                    if (OnVolumeChanged != null)
                        OnVolumeChanged(this, new VolumeEventArgs(_currentVolume, VolumePercentage));
            } catch (Exception) { } // parse error, assume value is 'N/A' and proceed
        }

        void MuteChanged(Object source, MessageEventArgs e) {
            bool old = _isMuted;
            if (e.Message == "AMT00")
                _isMuted = false;
            else
                _isMuted = true;

            if (old != _isMuted)
                if (OnMuteChanged != null)
                    OnMuteChanged(this, new MuteEventArgs(_isMuted));
        }

        public void SubscribeVolumeChanges(ReceiverEventHandler e) {
            if (OnVolumeChanged == null)
                OnVolumeChanged = new ReceiverEventHandler(e);
            else
                OnVolumeChanged += new ReceiverEventHandler(e);
        }

        public void SubscribeMuteChanges(ReceiverEventHandler e) {
            if (OnMuteChanged == null)
                OnMuteChanged = new ReceiverEventHandler(e);
            else
                OnMuteChanged += new ReceiverEventHandler(e);
        }
    }
}
