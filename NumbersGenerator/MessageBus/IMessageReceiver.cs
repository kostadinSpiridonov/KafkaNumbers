using System.Threading.Tasks;

namespace MessageBus
{
    public interface IMessageReceiver
    {
        Task<T> Receive<T>(string topic);
    }
}
