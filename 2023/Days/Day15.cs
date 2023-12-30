using Common;

namespace _2023.Days
{
    public class Day15 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetFullInput(nameof(Day15));

            var initSteps = input.Split(',', StringSplitOptions.RemoveEmptyEntries);
            var sequences = initSteps.Select(x => new InitSequnece(x));
            var boxes = FollowSequence(sequences);

            int power = CalculateFocusingPower(boxes);

            return (nameof(Day15), sequences.Sum(x => x.FullHash()).ToString(), power.ToString());
        }

        private static Dictionary<int, List<Lens>> FollowSequence(IEnumerable<InitSequnece> sequences)
        {
            var boxes = new Dictionary<int, List<Lens>>();
            foreach (var sequence in sequences)
            {
                var labelHash = sequence.LabelHash();
                boxes.TryAdd(labelHash, new List<Lens>());

                var lens = sequence.Lens;
                if (sequence.Instruction.Equals("remove"))
                {
                    boxes[labelHash] = boxes[labelHash].Where(x => !x.Hash.Equals(lens.Hash)).ToList();
                }
                else
                {
                    var boxInList = boxes[labelHash].FirstOrDefault(x => x.Hash.Equals(lens.Hash));

                    if (boxInList == null)
                    {
                        boxes[labelHash].Add(lens);
                    }
                    else
                    {
                        boxInList.FocalLength = lens.FocalLength;
                    }
                }
            }

            return boxes;
        }

        private static int CalculateFocusingPower(Dictionary<int, List<Lens>> boxes)
        {
            var sum = 0;
            foreach (var box in boxes.Where(x => x.Value.Any()))
            {
                var slots = box.Value;
                for (var i = 0; i < slots.Count; i++)
                {
                    var boxHash = 1 + box.Key;
                    var focalLen = slots[i].FocalLength;
                    var slotPosition = 1 + i;
                    sum += (boxHash * focalLen * slotPosition);
                }
            }

            return sum;
        }
    }

    public class InitSequnece
    {
        private readonly string input;
        public InitSequnece(string input) 
        {
            this.input = input;
            if(input.Contains('=')) {
                var parts = input.Split('=');
                Instruction = "add";
                Lens = new Lens(parts[0], int.Parse(parts[1]));
            } 
            else
            {
                var parts = input.Split('-');
                Lens = new Lens(parts[0], 0);
                Instruction = "remove";
            }
        }

        public string Instruction { get; private set; }

        public Lens Lens { get; private set; }

        private static int Hash(string str)
        {
            var currentValue = 0;
            for (int i = 0; i < str.Length; i++)
            {
                var asci = (int)str[i];
                currentValue += asci;
                currentValue *= 17;
                currentValue %= 256;
            }

            return currentValue;
        }

        public int LabelHash() => Hash(Lens.Hash);

        public int FullHash() => Hash(input);
    }

    public record Lens
    {
        public Lens(string name, int length)
        {
            Hash = name;
            FocalLength = length;
        }

        public int FocalLength { get; set; }
        public string Hash { get; private set; }
    }
}
