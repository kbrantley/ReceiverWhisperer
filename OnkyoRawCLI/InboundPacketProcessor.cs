using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace OnkyoRawCLI {
    class InboundPacketProcessor {
        private volatile bool _shouldStop = false;
        private NetworkStream _stream;

        public void SetStream(NetworkStream stream) {
            if (stream != null)
                _stream = stream;
        }

        public void ProcessPackets() {
            while (!_shouldStop) {
                InboundPacket packet = new InboundPacket(_stream);
                ProcessPacket(packet);
            }
        }

        public void RequestStop() {
            _shouldStop = true;
        }

        private void ProcessPacket(InboundPacket packet) {
            Console.WriteLine("-->" + packet.Message);
        }
    }
}
