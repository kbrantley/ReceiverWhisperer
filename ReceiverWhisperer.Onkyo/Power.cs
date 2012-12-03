using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReceiverWhisperer.Core;

namespace ReceiverWhisperer.Onkyo {
    class Power : PowerManagement {
        private OnkyoReceiver _receiver;
        private bool _isPowered;
        private int _displayBrightness;
        private int _displayBrightnessMax;
        private int _displayBrightnessMin;
        private int _sleepTimeRemaining;

        public bool IsPowered {
            get { return _isPowered; }
            set { SetPower(value); }
        }

        public int DisplayBrightness {
            get { return _displayBrightness;  }
            set { SetDisplayBrightness(value); }
        }

        public double DisplayBrightnessPercent {
            get { return (double)_displayBrightness / (double)_displayBrightnessMax; }
            set { SetDisplayBrightness(value); }
        }

        public int SleepTimer {
            get { return _sleepTimeRemaining;  }
            set { SetSleepTimer(value);  }
        }

        public Power(OnkyoReceiver receiver) {
            _displayBrightnessMax = 100;
            _displayBrightnessMin = 0;
            _receiver = receiver;
            _receiver._ipp.Subscribe("PWR", PowerEvent);
            _receiver._ipp.Subscribe("DIM", DisplayBrightnessEvent);
            _receiver._ipp.Subscribe("SLP", SleepTimerEvent);
            _receiver._ipp.Subscribe("OnConnect", OnConnect);
        }

        private void OnConnect(Object sender, EventArgs e) {
            new OutboundPacket(_receiver, "PWRQSTN");
            new OutboundPacket(_receiver, "DIMQSTN");
            new OutboundPacket(_receiver, "SLPQSTN");
        }

        private void SetPower(bool powered) {
            if (powered)
                new OutboundPacket(_receiver, "PWR01");
            else
                new OutboundPacket(_receiver, "PWR00");
        }

        private void SetDisplayBrightness(int brightness) {
            if (brightness > 66)
                new OutboundPacket(_receiver, "DIM00");
            else if (brightness <= 66 && brightness > 33)
                new OutboundPacket(_receiver, "DIM01");
            else if (brightness <= 33 && brightness > 1)
                new OutboundPacket(_receiver, "DIM02");
            else if (brightness == 1)
                new OutboundPacket(_receiver, "DIM03");
            else if (brightness == 0)
                new OutboundPacket(_receiver, "DIM08");
        }

        private void SetDisplayBrightness(double brightness) {
            SetDisplayBrightness((int)Math.Floor(_displayBrightnessMax * brightness));
        }

        public void DisplayBrightnessStep() {
            new OutboundPacket(_receiver, "DIMDIM");
        }

        private void SetSleepTimer(int time) {
            if (time == 0)
                new OutboundPacket(_receiver, "SLPOFF");
            else
                new OutboundPacket(_receiver, "SLP" + time.ToString("X2"));
        }

        private void PowerEvent(Object sender, MessageEventArgs e) {
            if (e.Message == "PWR00")
                _isPowered = false;
            else if (e.Message == "PWR01")
                _isPowered = true;
        }

        private void DisplayBrightnessEvent(Object sender, MessageEventArgs e) {
            if (e.Message == "DIM00")
                _displayBrightness = 100;
            else if (e.Message == "DIM01")
                _displayBrightness = 66;
            else if (e.Message == "DIM02")
                _displayBrightness = 33;
            else if (e.Message == "DIM03")
                _displayBrightness = 1;
            else if (e.Message == "DIM08")
                _displayBrightness = 0;
        }

        private void SleepTimerEvent(Object sender, MessageEventArgs e) {
            try {
                _sleepTimeRemaining = Int32.Parse(e.Message.Substring(3), System.Globalization.NumberStyles.HexNumber);
            } catch (Exception) { } // assume that the response was 'SLPN/A' and disregard
        }
    }
}
