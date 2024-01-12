using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.Producer.Domain.Command;

public static class CommandProducerSendAsync
{
    public static async Task Send(IProducer<Null, string> producer, string topic, Message<Null, string> message)
    {
        await Task.Factory.StartNew(() =>
        {
            producer.ProduceAsync(topic, message);
        });
    }
}
