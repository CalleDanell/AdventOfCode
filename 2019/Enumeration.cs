namespace _2019
{
    public enum OperationCode
    {
        Add = 01,
        Multiply = 02,
        Input = 03,
        Output = 04,
        JumpIfTrue = 05,
        JumpIfFalse = 06,
        LessThan = 07,
        Equals = 08,
        Stop = 99
    }

    public enum ParameterMode
    {
        PositionMode = 1,
        ImmediateMode = 2
    }

    public enum ComputerState
    {
        Running = 1,
        Stopped = 2,
        Paused = 3
    }
}