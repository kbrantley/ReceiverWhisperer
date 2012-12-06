using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace ReceiverWhisperer.Onkyo {
    class OutboundPacket {
        private const int _packetHeaderTotalSize = 16;
        private static byte[] _packetHeader = { 0x49, 0x53, 0x43, 0x50, 0x00, 0x00, 0x00, 0x10 };
        private static byte[] _packetFooter = { 0x0d, 0x0a };
        private const byte _protocolVersion = 0x01;

        public byte[] Packet { private set; get; }

        public OutboundPacket(OnkyoReceiver receiver, String command) {
            NetworkStream stream = receiver._stream;
            command = "!1" + command;
            int dataSize = command.Length + _packetFooter.Length;
            int packetSize = _packetHeaderTotalSize + dataSize;

            byte[] message = new byte[packetSize];
            int pos = 0;

            foreach (byte b in _packetHeader)
                message[pos++] = b;

            foreach (byte b in BitConverter.GetBytes(dataSize).Reverse())
                message[pos++] = b;

            message[pos++] = _protocolVersion;
            pos += 3;

            foreach (byte b in System.Text.Encoding.ASCII.GetBytes(command))
                message[pos++] = b;

            foreach (byte b in _packetFooter)
                message[pos++] = b;
            stream.Write(message, 0, message.Length);
            try { System.Threading.Thread.Sleep(100); } catch (Exception) { }
        }
    }
}

