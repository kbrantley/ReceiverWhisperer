using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReceiverWhisperer.Core;

namespace ReceiverWhisperer.Onkyo {
    public class OnkyoInputSelector : InputSelector {
        OnkyoReceiver _receiver;
        private OnkyoInput _currentInput;
        private List<Input> _validInputs;
        public Input ActiveInput { get { return _currentInput; } set { value.Activate(); } }

        public event InputEventHandler OnInputChanged;

        public IEnumerable<Input> AvailableInputs {
            get { if (_validInputs == null) BuildInputs(); return _validInputs; }
        }

        public OnkyoInputSelector(OnkyoReceiver receiver) {
            _receiver = receiver;
            BuildInputs();
        }

        public void SwitchInput(OnkyoInput input) {
            if (input != _currentInput) {
                _currentInput = input;
                if (OnInputChanged != null)
                    OnInputChanged(this, new InputEventArgs(input));
            }
        }

        public void SubscribeInputChanges(InputEventHandler e) {
            if (OnInputChanged == null)
                OnInputChanged = new InputEventHandler(e);
            else
                OnInputChanged += new InputEventHandler(e);
        }

        private void BuildInputs() {
            if (_validInputs == null) {
                _validInputs = new List<Input>();
                _validInputs.Add(new OnkyoInput(_receiver, this, ReceiverWhisperer.Onkyo.Properties.Resources.SLI01, "SLI01"));
                _validInputs.Add(new OnkyoInput(_receiver, this, ReceiverWhisperer.Onkyo.Properties.Resources.SLI02, "SLI02"));
                _validInputs.Add(new OnkyoInput(_receiver, this, ReceiverWhisperer.Onkyo.Properties.Resources.SLI03, "SLI03"));
                _validInputs.Add(new OnkyoInput(_receiver, this, ReceiverWhisperer.Onkyo.Properties.Resources.SLI04, "SLI04"));
                _validInputs.Add(new OnkyoInput(_receiver, this, ReceiverWhisperer.Onkyo.Properties.Resources.SLI05, "SLI05"));
                _validInputs.Add(new OnkyoInput(_receiver, this, ReceiverWhisperer.Onkyo.Properties.Resources.SLI07, "SLI07"));
                _validInputs.Add(new OnkyoInput(_receiver, this, ReceiverWhisperer.Onkyo.Properties.Resources.SLI08, "SLI08"));
                _validInputs.Add(new OnkyoInput(_receiver, this, ReceiverWhisperer.Onkyo.Properties.Resources.SLI10, "SLI10"));
                _validInputs.Add(new OnkyoInput(_receiver, this, ReceiverWhisperer.Onkyo.Properties.Resources.SLI23, "SLI23"));
                _validInputs.Add(new OnkyoInput(_receiver, this, ReceiverWhisperer.Onkyo.Properties.Resources.SLI24, "SLI24"));
                _validInputs.Add(new OnkyoInput(_receiver, this, ReceiverWhisperer.Onkyo.Properties.Resources.SLI25, "SLI25"));
                _validInputs.Add(new OnkyoInput(_receiver, this, ReceiverWhisperer.Onkyo.Properties.Resources.SLI26, "SLI26"));
                _validInputs.Add(new OnkyoInput(_receiver, this, ReceiverWhisperer.Onkyo.Properties.Resources.SLI27, "SLI27"));
                _validInputs.Add(new OnkyoInput(_receiver, this, ReceiverWhisperer.Onkyo.Properties.Resources.SLI28, "SLI28"));
                _validInputs.Add(new OnkyoInput(_receiver, this, ReceiverWhisperer.Onkyo.Properties.Resources.SLI29, "SLI29"));
                _validInputs.Add(new OnkyoInput(_receiver, this, ReceiverWhisperer.Onkyo.Properties.Resources.SLI2A, "SLI2A"));
                _validInputs.Add(new OnkyoInput(_receiver, this, ReceiverWhisperer.Onkyo.Properties.Resources.SLI2B, "SLI2B"));

            }
        }
    }
}
