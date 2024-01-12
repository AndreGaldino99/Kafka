using Confluent.Kafka;
using Kafka.Consumer;
using Kafka.Utils;

namespace Kafka.API;

public class KafkaWorkerConsumer(ILogger<KafkaWorkerConsumer> logger) : BackgroundService
{
    private readonly ILogger<KafkaWorkerConsumer> _logger = logger;

    private readonly ConsumerConfig _conf = new()
    {
        GroupId = "test-consumer-group",
        BootstrapServers = KafkaUtils.BootstrapServer,
        AutoOffsetReset = AutoOffsetReset.Earliest
    };

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
