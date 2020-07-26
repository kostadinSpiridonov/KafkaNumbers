using Confluent.Kafka;
using MessageBus;
using MessageBus.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NumbersHandler.Handlers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NumbersHandler
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ServiceProvider serviceProvider = BuildServiceProvider();

            IRunner runner = serviceProvider.GetService<IRunner>();
            await runner.Run(CancellationToken.None);
        }

        private static ServiceProvider BuildServiceProvider()
        {
            IConfiguration Configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .Build();

            var services = new ServiceCollection();
            services.Configure<NumbersConfiguration>(Configuration);
            services.AddTransient<IRunner, Runner>();
            services.AddTransient<IMessageReceiver, KafkaMessageReceiver>();
            services.AddTransient<IMessageHandler<NumberMessage>, NumberMessageHandler>();
            services.Configure<MessageBusConfiguration>(Configuration.GetSection("Kafka"));

            return services.BuildServiceProvider();
        }
    }
}
