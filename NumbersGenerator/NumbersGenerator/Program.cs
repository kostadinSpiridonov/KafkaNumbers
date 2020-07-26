using MessageBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NumbersGenerator.Timers;
using System.Threading;
using System.Threading.Tasks;

namespace NumbersGenerator
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
            services.AddTransient<IMessagePublisher,KafkaMessagePublisher>();
            services.AddTransient<IRunner, Runner>();
            services.AddTransient<IGenerator, Generator>();
            services.AddTransient<ILevelTimer, DegreeLevelTimer>();
            services.Configure<MessageBusConfiguration>(Configuration.GetSection("Kafka"));
            services.Configure<GeneratorConfiguration>(Configuration);


            return services.BuildServiceProvider();
        }
    }
}
