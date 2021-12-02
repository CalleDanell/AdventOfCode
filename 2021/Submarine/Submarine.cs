namespace _2021.Submarine
{
    public class Submarine
    {
        public Submarine(int horizontalPosition, int depth, int aim)
        {
            HorizontalPosition = horizontalPosition;
            Depth = depth;
            Aim = aim;
        }

        public int Depth { get; private set; }
        public int HorizontalPosition { get; private set; }
        public int Aim { get; private set; }

        public void Move(NavigationInstruction navigationInstruction)
        {
            switch (navigationInstruction.Navigation)
            {
                case "forward":
                    HorizontalPosition += navigationInstruction.Steps;
                    break;
                case "up":
                    Depth -= navigationInstruction.Steps;
                    break;
                case "down":
                    Depth += navigationInstruction.Steps;
                    break;
            }
        }

        public void MoveWithAim(NavigationInstruction navigationInstruction)
        {
            switch (navigationInstruction.Navigation)
            {
                case "forward":
                    HorizontalPosition += navigationInstruction.Steps;
                    Depth += Aim * navigationInstruction.Steps;
                    break;
                case "up":
                    Aim -= navigationInstruction.Steps;
                    break;
                case "down":
                    Aim += navigationInstruction.Steps;
                    break;
            }
        }
        
        public int PositionResult => Depth * HorizontalPosition;
    }
}