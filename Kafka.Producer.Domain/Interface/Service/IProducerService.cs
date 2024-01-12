using Confluent.Kafka;

namespace Kafka.Producer.Domain.Interface.Service;

public interface IProducerService
{
    Task Produce(List<Message<Null, string>> messages);
}
