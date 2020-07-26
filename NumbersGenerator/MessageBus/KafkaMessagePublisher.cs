using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MessageBus
{
    public class KafkaMessagePublisher : KafkaClient, IMessagePublisher
    {
        private readonly IProducer<Null, string> _producer;

        public KafkaMessagePublisher(IOptions<MessageBusConfiguration> configuration)
            : base(configuration)
        {
            _producer = new ProducerBuilder<Null, string>(BuildConsumerConfiguration())
                .Build();
            
        }

        public Task Publish<T>(T message, string topic)
        {
            string value = JsonConvert.SerializeObject(message);
            return _producer.ProduceAsync(topic, new Message<Null, string> { Value = value });
        }

        public void Dispose()
        {
            _producer.Dispose();
        }
    }
}
