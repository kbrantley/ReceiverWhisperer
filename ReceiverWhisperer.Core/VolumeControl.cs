using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReceiverWhisperer.Core {
    public interface VolumeControl {
        int Volume { get; set; }
        double VolumePercentage { get; set; }
        int MinimumVolume { get; }
        int MaximumVolume { get; }
        bool Muted { get; set; }

        void StepUp();
        void StepDown();
    }
}
