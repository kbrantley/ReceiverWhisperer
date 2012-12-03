using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace ReceiverWhisperer.Onkyo {

    public delegate void InboundPacketProcessorHandler(Object sender, MessageEventArgs e);

    public class MessageEventArgs : EventArgs {
        public String Message { private set; get; }
    
        public MessageEventArgs(String message) {
            this.Message = message;
        }
    }

    class InboundPacketProcessor {
        public Dictionary<String, InboundPacketProcessorHandler> eventDictionary;
        private volatile bool _shouldStop = false;
        private OnkyoReceiver _receiver;

        public InboundPacketProcessor(OnkyoReceiver receiver) {
            eventDictionary = new Dictionary<string, InboundPacketProcessorHandler>();
            _receiver = receiver;
        }

        public void ProcessPacketLoop() {
            while (!_shouldStop) {
                try {
                    InboundPacket packet = new InboundPacket(_receiver._stream);
                    ProcessPacket(packet);
                } catch (System.IO.IOException) {
                    return;
                }
            }
        }

        public void RequestStop() {
            _shouldStop = true;
        }

        private void ProcessPacket(InboundPacket p) {
            if (eventDictionary.ContainsKey(p.Prefix))
                eventDictionary[p.Prefix](this, new MessageEventArgs(p.Message));
        }

        internal void Subscribe(String prefix, InboundPacketProcessorHandler handler) {
            if (!eventDictionary.ContainsKey(prefix))
                eventDictionary[prefix] = new InboundPacketProcessorHandler(handler);
            else
                eventDictionary[prefix] += new InboundPacketProcessorHandler(handler);
        }

        internal void Unsubscribe(String prefix, InboundPacketProcessorHandler handler) {
            if (eventDictionary.ContainsKey(prefix))
                eventDictionary[prefix] -= new InboundPacketProcessorHandler(handler);
        }
    }
}
