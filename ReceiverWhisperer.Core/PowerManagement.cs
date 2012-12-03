using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReceiverWhisperer.Core {
    public interface PowerManagement {
        bool IsPowered { get; set; }
        int DisplayBrightness { get; set; }
        double DisplayBrightnessPercent { get; set; }
        void DisplayBrightnessStep();
        int SleepTimer { get; set; }
    }
}
