using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;

namespace MessageBus
{
    public class KafkaMessageReceiver : KafkaClient, IMessageReceiver, IDisposable
    {
        private readonly IConsumer<Ignore, string> _consumer;

        public KafkaMessageReceiver(IOptions<MessageBusConfiguration> configuration)
            : base(configuration)
        {
            _consumer = new ConsumerBuilder<Ignore, string>(BuildConsumerConfiguration()).Build();
        }

        public Task<T> Receive<T>(string topic)
        {
            _consumer.Subscribe(topic);

            ConsumeResult<Ignore, string> consumeResult = _consumer.Consume();
            T value = JsonConvert.DeserializeObject<T>(consumeResult.Message.Value);

            return Task.FromResult(value);
        }

        public void Dispose()
        {
            _consumer.Close();
            _consumer.Dispose();
        }
    }
}
