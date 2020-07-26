using MessageBus;
using System.Threading.Tasks;
using MessageBus.Messages;
using NumbersGenerator.Timers;
using System;
using System.Threading;
using Microsoft.Extensions.Options;

namespace NumbersGenerator
{
    public class Runner : IRunner
    {
        private readonly IMessagePublisher _publisher;
        private readonly ILevelTimer _timer;
        private readonly IGenerator _generator;
        private readonly GeneratorConfiguration _configuration;

        public Runner(
            IMessagePublisher publisher,
            ILevelTimer timer,
            IGenerator generator,
            IOptions<GeneratorConfiguration> options)
        {
            _publisher = publisher;
            _timer = timer;
            _generator = generator;
            _configuration = options.Value;
        }

        public async Task Run(CancellationToken cancellationToken)
        {
            _timer.Initialize(_configuration.InitialLevelTicks, _configuration.LevelPeriod, _configuration.MaxLevel);
            _timer.OnValueHit += (o, e) => SendEvent().Wait();
            _timer.Start();

            while (!cancellationToken.IsCancellationRequested)
            {

            }
        }

        private async Task SendEvent()
        {
            int number = await _generator.Generate(0, 100);
            await _publisher.Publish(new NumberMessage { Value = number }, MeesageBusStaticTopics.NUMBERS_TOPIC);
        }
    }
}
