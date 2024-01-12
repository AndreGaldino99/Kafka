using Confluent.Kafka;
using Kafka.Utils;

namespace Kafka.Consumer;

public static class KafkaExtension
{
    public static async Task ExecuteKafka(this ConsumerConfig _conf)
    {
        using var c = new ConsumerBuilder<Ignore, string>(_conf).Build();
        {
            c.Subscribe(KafkaUtils.TopicQueue);

            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            try
            {
                while (true)
                {
                    try
                    {
                        await Task.Delay(1000);
                        await ConsumerClass.Consume(c, cts);
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Error occured: {e.Error.Reason}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                c.Close();
            }
        }
    }
}
