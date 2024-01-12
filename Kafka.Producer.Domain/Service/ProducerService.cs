using Confluent.Kafka;
using Kafka.Producer.Domain.Command;
using Kafka.Producer.Domain.Interface.Service;
using Kafka.Utils;

namespace Kafka.Producer.Domain.Service
{
    public class ProducerService : IProducerService
    {
        public readonly ProducerConfig _config = new ProducerConfig
        {
            BootstrapServers = KafkaUtils.BootstrapServer
        };

        public async Task Produce(List<Message<Null, string>>  messages)
        {
            using (var producer = new ProducerBuilder<Null, string>(_config).Build())
            {
                var topic = KafkaUtils.TopicQueue;

                var tasks = new List<Task>();

                foreach (var message in messages)
                {
                    var deliveryReport = CommandProducerSendAsync.Send(producer, topic, message);

                    tasks.Add(deliveryReport);
                }

                try
                {
                    await Task.WhenAll(tasks);
                    Console.WriteLine("Mensagens enviadas com sucesso em lote!");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Falha ao enviar as mensagens: {e.Error.Reason}");
                }
            }
        }
    }
}
