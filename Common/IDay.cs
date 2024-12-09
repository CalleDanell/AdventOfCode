using System.Threading.Tasks;

namespace Common
{
    public interface IDay
    {
        Task<(string, string, string)> Solve();
    }
}