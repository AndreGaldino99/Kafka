using Confluent.Kafka;
using Kafka.Utils;
using Newtonsoft.Json;
using System.Text;

namespace Kafka.Consumer;

public static class ConsumerClass
{
    public static async Task Consume(IConsumer<Ignore, string> c, CancellationTokenSource cts)
    {
        string messageValue = string.Empty;
        try
        {
            var cr = c.Consume(cts.Token);
            messageValue = cr.Message.Value.ToString() ?? "";
            messageValue.SendAsync();
        }
        catch (Exception e)
        {
            await messageValue.ProduceError($"Erro Consumindo negócio - {e.Message}");
        }
    }
}

public static class _00000000000_Send
{
    public static async void SendAsync(this string jsonData)
    {
        using HttpClient client = new();
        string apiUrl = "https://localhost:7045/api/ProcessLog";
        try
        {
            HttpResponseMessage response = await client.PostAsync(apiUrl, new StringContent(jsonData, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
            else
            {
                await jsonData.ProduceError(response.ReasonPhrase ?? "");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }
}

public static class _00000000000_SendError
{
    private readonly static IProducer<Null, string> _producerBuild =
        new ProducerBuilder<Null, string>(new ProducerConfig
        {
            BootstrapServers = KafkaUtils.BootstrapServer,
        }).Build();
    public static async Task ProduceError(this string messageValue, string errorMessageValue)
    {
        var errorMessage = new
        {
            Value = errorMessageValue,
            Message = messageValue
        };
        await _producerBuild.ProduceAsync(KafkaUtils.TopicQueueError, new Message<Null, string> { Value = JsonConvert.SerializeObject(errorMessage) });
    }
}
