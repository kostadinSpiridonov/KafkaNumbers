using MessageBus.Messages;
using System.Threading.Tasks;

namespace NumbersHandler.Handlers
{
    public interface IMessageHandler<T>
    {
        public Task Handle(T message);
    }
}
