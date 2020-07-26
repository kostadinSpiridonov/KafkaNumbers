using MessageBus;
using MessageBus.Messages;
using NumbersHandler.Handlers;
using System.Threading;
using System.Threading.Tasks;

namespace NumbersHandler
{
    public class Runner : IRunner
    {
        private readonly IMessageReceiver _receiver;
        private readonly IMessageHandler<NumberMessage> _handler;

        public Runner(
            IMessageReceiver receiver,
            IMessageHandler<NumberMessage> handler)
        {
            _receiver = receiver;
            _handler = handler;
        }

        public async Task Run(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var message = await _receiver.Receive<NumberMessage>(MeesageBusStaticTopics.NUMBERS_TOPIC);
                if (message != null)
                {
                    await _handler.Handle(message);
                }
            }
        }
    }
}
