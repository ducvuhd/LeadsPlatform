using Confluent.Kafka;
using System;

namespace APVN.LeadsPlatform.Logging.KafkaTarget
{
    public class KafkaProducerSync : KafkaProducerAbstract
    {
        public KafkaProducerSync(string brokers, string lingerMs) : base(brokers, lingerMs)
        {
        }

        public override void Produce(ref string topic, ref byte[] data)
        {
            Producer.Produce(topic, new Message<string, byte[]>
            {
                Key = Guid.NewGuid().ToString(),
                Value = data
            });
        }
    }
}