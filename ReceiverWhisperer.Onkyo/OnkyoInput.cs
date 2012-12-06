using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReceiverWhisperer.Core;

namespace ReceiverWhisperer.Onkyo {
    public class OnkyoInput : ReceiverWhisperer.Core.Input {
        private OnkyoReceiver _receiver;
        private OnkyoInputSelector _inputSelector;
        private string _inputIdentity;
        public string Name { get; private set; }

        public void Activate() {
            new OutboundPacket(_receiver, _inputIdentity);
        }

        public OnkyoInput(OnkyoReceiver receiver, OnkyoInputSelector inputSelector, String name, String inputIdentity) {
            _receiver = receiver;
            _inputSelector = inputSelector;
            Name = name;
            _inputIdentity = inputIdentity;
            _receiver._ipp.Subscribe("SLI", InputChanged);
        }

        void InputChanged(Object source, MessageEventArgs e) {
            if (e.Message == _inputIdentity)
                _inputSelector.SwitchInput(this);
        }
    }
}
