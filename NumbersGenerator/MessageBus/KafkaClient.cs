using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System.Net;

namespace MessageBus
{
    public class KafkaClient
    {
        private readonly MessageBusConfiguration _configuration;

        public KafkaClient(IOptions<MessageBusConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }

        protected ProducerConfig BuildProducerConfiguration()
        {
            return new ProducerConfig
            {
                BootstrapServers = $"{_configuration.Host}:{_configuration.Port}",
                ClientId = Dns.GetHostName(),
            };
        }

        protected ConsumerConfig BuildConsumerConfiguration()
        {
            return new ConsumerConfig
            {
                BootstrapServers = $"{_configuration.Host}:{_configuration.Port}",
                ClientId = Dns.GetHostName(),
                GroupId = "default",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
        }
    }
}
