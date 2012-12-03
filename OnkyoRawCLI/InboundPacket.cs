using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace OnkyoRawCLI {
    class InboundPacket {
        public String Message { private set; get; }
        public InboundPacket(NetworkStream stream) {
            for (int i = 0; i < 8; i++)
                stream.ReadByte();

            byte[] dataSizeRaw = new byte[4];
            for (int i = 0; i < 4; i++)
                dataSizeRaw[i] = (byte)stream.ReadByte();

            byte[] dataSizeBE = new byte[4];
            dataSizeBE[0] = dataSizeRaw[3];
            dataSizeBE[1] = dataSizeRaw[2];
            dataSizeBE[2] = dataSizeRaw[1];
            dataSizeBE[3] = dataSizeRaw[0];

            int dataSize = BitConverter.ToInt32(dataSizeBE, 0);
            for (int i = 0; i < 4; i++)
                stream.ReadByte();

            byte[] dataPacketRaw = new byte[dataSize];

            for (int i = 0; i < dataSize; i++)
                dataPacketRaw[i] = (byte)stream.ReadByte();

            byte[] dataPacketTrimmed = new byte[dataPacketRaw.Length - 3];
            for (int i = 0; i < dataPacketTrimmed.Length; i++)
                dataPacketTrimmed[i] = dataPacketRaw[i];

            Message = System.Text.Encoding.ASCII.GetString(dataPacketTrimmed).Substring(2);
        }
    }
}
