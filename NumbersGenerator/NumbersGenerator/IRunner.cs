using System.Threading.Tasks;
using System.Threading;

namespace NumbersGenerator
{
    public interface IRunner
    {
        Task Run(CancellationToken cancellationToken);
    }
}
