using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

/* Name this 'Receiver Whisperer' */

namespace OnkyoRawCLI {
    class Program {
        static void Main(string[] args) {

            if (!(args.Length == 1 || args.Length == 2)) {
                Console.WriteLine("Please provide IP (port optional) of the receiver");
                Environment.Exit(-1);
            }

            TcpClient receiverSocket = new TcpClient();
            int port = 60128;
            if (args.Length == 2) {
                port = System.Convert.ToInt32(args[1]);
            }
            receiverSocket.Connect(args[0], port);
            NetworkStream receiverStream = receiverSocket.GetStream();

            InboundPacketProcessor ipp = new InboundPacketProcessor();
            ipp.SetStream(receiverStream);
            Thread inboundThread = new Thread(ipp.ProcessPackets);
            inboundThread.Start();

            bool quitting = false;
            while (!quitting) {
                String msg = Console.ReadLine();
                if (msg == "quit") {
                    quitting = true;
                    ipp.RequestStop();
                    new OutboundPacket(receiverStream, "MVLQSTN");
                } else if (msg.Trim().Length > 0) {
                    Console.WriteLine("<--" + msg);
                    new OutboundPacket(receiverStream, msg);
                }
            }
            inboundThread.Join();
            receiverSocket.Close();
        }
    }
}
