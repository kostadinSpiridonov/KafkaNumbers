using System.Threading.Tasks;
using System.Threading;

namespace NumbersHandler
{
    public interface IRunner
    {
        Task Run(CancellationToken cancellationToken);
    }
}
