using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using ReceiverWhisperer.Core;

namespace ReceiverWhisperer.Onkyo {
    public class OnkyoReceiver : Receiver {
        private TcpClient _socket;
        internal NetworkStream _stream;
        internal InboundPacketProcessor _ipp;
        private VolumeControl _volume;
        private Power _power;
        private Thread _networkThread;

        public string Name { get; private set; }
        public string IPAddress { get; private set; }
        public int Port { get; private set; }
        public bool IsConnected { get; private set; }

        public IEnumerable<Input> ValidInputs {
            get { throw new NotImplementedException(); }
        }
        public Input CurrentInput {
            get { throw new NotImplementedException(); }
        }

        public VolumeControl VolumeControl { get { return _volume; } }
        public PowerManagement PowerManagement { get { return _power; } }

        public OnkyoReceiver(String Name, String IPAddress, int Port) {
            this.Name = Name;
            this.IPAddress = IPAddress;
            this.Port = Port;
            _ipp = new InboundPacketProcessor(this);
            Connect();
            _volume = new Volume(this);
            _power = new Power(this);
        }

        public void Connect() {
            if (!IsConnected) {
                _socket = new TcpClient();
                _socket.Connect(IPAddress, Port);
                _stream = _socket.GetStream();
                _networkThread = new Thread(_ipp.ProcessPacketLoop);
                _networkThread.Start();
                IsConnected = true;
                System.Threading.Thread.Sleep(2000);
            }
        }

        public void Disconnect() {
            if (IsConnected) {
                _ipp.RequestStop();
                _stream.Close();
                _socket.Close();
                _networkThread.Join();
                IsConnected = false;
            }
        }
    }
}
