using Common;
using System.Text.RegularExpressions;

namespace _2024.Days
{
    public class Day13 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var day = GetType().Name;
            var input = await InputHandler.GetInputGroupWithNewLineSeparation(day, string.Empty);

            double sumOne = 0;
            double sumTwo = 0;
            foreach (var item in input)
            {
                (double A, double B) = FindAandB(item, 0);
                if (A <= 100 && A % 1 == 0 && B % 1 == 0 && B <= 100)
                {
                    sumOne += ((A * 3) + B);
                }

                (double A2, double B2) = FindAandB(item, 10000000000000);
                if (A2 % 1 == 0 && B2 % 1 == 0)
                {
                    sumTwo += ((A2 * 3) + B2);
                }
            }

            var partOne = sumOne;
            var partTwo = sumTwo;
            return (day, partOne.ToString(), partTwo.ToString());
        }

        private static (double A, double B) FindAandB(string machine, long extra)
        {
            var x = Regex.Matches(machine, "(X\\+|X\\=)(\\d+)").Select(x => long.Parse(x.Groups[2].Value)).ToList();
            var y = Regex.Matches(machine, "(Y\\+|Y\\=)(\\d+)").Select(x => long.Parse(x.Groups[2].Value)).ToList();

            long buttonAx = x[0];
            long buttonBx = x[1];
            long targetX = x[2] + extra;

            long buttonAy = y[0];
            long buttonBy = y[1];
            long targetY = y[2] + extra;

            // s = (pxby - pyby) / (axby - aybx) 
            double s = ((targetX * buttonBy) - (targetY * buttonBx)) / ((buttonAx * buttonBy) - (buttonAy * buttonBx));
            double t = (targetX - (buttonAx * s)) / buttonBx;

            if(buttonAx*s + buttonBx*t == targetX && buttonAy * s + buttonBy * t == targetY)
            {
                return (s, t);
            }

            return (0, 0);
        }
    }
}