using System.Threading.Tasks;

namespace MessageBus
{
    public interface IMessagePublisher
    {
        Task Publish<T>(T message, string topic);
    }
}
