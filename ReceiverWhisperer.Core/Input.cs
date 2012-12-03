using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReceiverWhisperer.Core {
    public interface Input {
        String Name { get; }
        void Activate();
    }
}
