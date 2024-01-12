using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.Producer.Domain.Interface.Service;

public interface IProducerService
{
    Task Produce(List<Message<Null, string>> messages);
}
