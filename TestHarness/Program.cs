using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReceiverWhisperer.Core;
using ReceiverWhisperer.Onkyo;

namespace TestHarness {
    class Program {
        static void Main(string[] args) {
            Receiver r = new OnkyoReceiver("TX-NR515", "10.254.254.98", 60128);
            Console.WriteLine("Power status: " + r.PowerManagement.IsPowered);
            r.PowerManagement.IsPowered = true;
            r.Disconnect();
        }
    }
}
