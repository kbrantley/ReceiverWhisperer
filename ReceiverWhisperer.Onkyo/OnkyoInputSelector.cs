using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReceiverWhisperer.Core;

namespace ReceiverWhisperer.Onkyo {
    public class OnkyoInputSelector : InputSelector {
        OnkyoReceiver _receiver;
        private OnkyoInput _currentInput;
        public Input ActiveInput { get { return _currentInput; } set { value.Activate(); } }

        public event ReceiverEventHandler OnInputChanged;

        public IEnumerable<Input> AvailableInputs {
            get { throw new NotImplementedException(); }
        }

        public OnkyoInputSelector(OnkyoReceiver receiver) {
            _receiver = receiver;
        }

        public void SwitchInput(OnkyoInput input) {
            if (input != _currentInput) {
                _currentInput = input;
                if (OnInputChanged != null)
                    OnInputChanged(this, new InputEventArgs(input));
            }
        }

        public void SubscribeInputChanges(ReceiverEventHandler e) {
            if (OnInputChanged == null)
                OnInputChanged = new ReceiverEventHandler(e);
            else
                OnInputChanged += new ReceiverEventHandler(e);
        }
    }
}
