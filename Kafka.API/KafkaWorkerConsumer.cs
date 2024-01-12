using Confluent.Kafka;
using Kafka.Consumer;
using Kafka.Utils;

namespace Kafka.API
{
    public class KafkaWorkerConsumer : BackgroundService
    {
        private readonly ILogger<KafkaWorkerConsumer> _logger;
        private readonly ConsumerConfig _conf = new ConsumerConfig
        {
            GroupId = "test-consumer-group",
            BootstrapServers = KafkaUtils.BootstrapServer,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        public KafkaWorkerConsumer(ILogger<KafkaWorkerConsumer> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _conf.ExecuteKafka();

                _logger.LogInformation("Worker em execução às: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
