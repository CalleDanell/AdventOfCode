using Common;

namespace _2023.Days
{
    public class Day09 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day09));

            var sequences = input.Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries));

            long sumLast = 0;
            long sumFirst = 0;
            
            foreach (var sequence in sequences) 
            {
                var subSeqsLast = GetSubSequences(sequence);
                var extrapolatedLast = GetLastExtrapolatedValues(subSeqsLast);
                sumLast += extrapolatedLast.Last();

                var subSeqsFirst = GetSubSequences(sequence);
                var extrapolatedFirst = GetFirstExtrapolatedValues(subSeqsFirst);
                sumFirst += extrapolatedFirst.First();
            }

            return (nameof(Day09), sumLast.ToString(), sumFirst.ToString());
        }

        private static IEnumerable<int> GetFirstExtrapolatedValues(IEnumerable<List<int>> subSeqs)
        {
            var seqs = new List<int>();
            var reversed = new List<List<int>>(subSeqs.Reverse().ToList());

            for (var i = 0; i < reversed.Count() - 1; i++)
            {
                var first = reversed.ElementAt(i).First();
                var nextFirst = reversed.ElementAt(i + 1).First();
                var nextValue = nextFirst - first;

                reversed.ElementAt(i + 1).Insert(0, nextValue);
                seqs.Insert(0, nextValue);
            }

            return seqs;
        }


        private static IEnumerable<int> GetLastExtrapolatedValues(IEnumerable<List<int>> subSeqs)
        {
            var seqs = new List<int>();
            var reversed = new List<List<int>> (subSeqs.Reverse().ToList());

            for (var i = 0; i < reversed.Count() - 1; i++)
            {
                var last = reversed.ElementAt(i).Last();
                var nextLast = reversed.ElementAt(i + 1).Last();
                var nextValue = last + nextLast;

                reversed.ElementAt(i + 1).Add(nextValue); 
                seqs.Add(nextValue);
            }

            return seqs;
        }

        private static IEnumerable<List<int>> GetSubSequences(IEnumerable<string> startSequence)
        {
            var current = startSequence.Select(int.Parse);
            var seqs = new List<List<int>>() { current.ToList() };

            while(true)
            {
                var nextSequence = new List<int>();
                for (var i = 0; i < current.Count() - 1; i++)
                {
                    var diff = current.ElementAt(i + 1) - current.ElementAt(i);
                    nextSequence.Add(diff);
                }
                seqs.Add(nextSequence);

                if(nextSequence.All(x => x == 0))
                {
                    break;
                }

                current = nextSequence;
            }
            
            return seqs;
        }
    }
}