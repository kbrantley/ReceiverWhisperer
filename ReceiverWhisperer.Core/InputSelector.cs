using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReceiverWhisperer.Core {
    public interface InputSelector {
        Input ActiveInput { get; set; }
        IEnumerable<Input> AvailableInputs { get; }

        void SubscribeInputChanges(InputEventHandler e);
    }
}
