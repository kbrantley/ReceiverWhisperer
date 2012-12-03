using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReceiverWhisperer.Core {
    public interface Receiver {
        String Name { get; }
        String IPAddress { get; }
        int Port { get; }

        bool IsConnected { get; }
        void Connect();
        void Disconnect();

        PowerManagement PowerManagement { get; }
        VolumeControl VolumeControl { get; }

        IEnumerable<Input> ValidInputs { get; }
        Input CurrentInput { get; }
    }
}
