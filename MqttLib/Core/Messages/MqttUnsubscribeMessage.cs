using System;
using System.Collections.Generic;
using System.Text;

namespace MqttLib.Core.Messages
{
    internal class MqttUnsubscribeMessage : MqttMessage
    {

        private string[] _topics;

        public MqttUnsubscribeMessage(ulong messageID, string[] topics) : base(MessageType.UNSUBSCRIBE)
        {
            _messageID = messageID;
            _topics = topics;
            base.msgQos = QoS.AtLeastOnce;
            int payloadLength = 0;

            // Work out the length of the payload
            foreach (string topic in _topics)
            {
                payloadLength += (2 + GetUTF8StringLength(topic));
            }
            variableHeaderLength = 8 + payloadLength;

        }

        protected override void SendPayload(System.IO.Stream str)
        {
            // Serialise the messageID
            WriteToStream(str, MessageID);
            //Write the payload
            foreach (string topic in _topics)
            {
                WriteToStream(str, topic);
            }
        }

        protected override void ConstructFromStream(System.IO.Stream str)
        {
            throw new Exception("Protocol does not support recieving UNSUBSCRIBE messages");
        }
    }
}
