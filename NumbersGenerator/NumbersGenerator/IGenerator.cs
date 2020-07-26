using System.Threading.Tasks;

namespace NumbersGenerator
{
    public interface IGenerator
    {
        Task<int> Generate(int min, int max);
    }
}