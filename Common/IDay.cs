using System.Threading.Tasks;

namespace Common.Days
{
    public interface IDay
    {
        Task<(string, string)> Solve(string day);
    }
}