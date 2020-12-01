using System.Threading.Tasks;

namespace AOC.Days
{
    public interface IDay
    {
        Task<(string, string)> Solve(string day);
    }
}