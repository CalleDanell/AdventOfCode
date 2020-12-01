namespace AOC
{
    public enum OperationCode
    {
        Add = 1,
        Multiply = 2,
        Input = 3,
        Output = 4,
        JumpIfTrue = 5,
        JumpIfFalse = 6,
        LessThan = 7,
        Equals = 8,
        Stop = 99
    }

    public enum ParameterMode
    {
        PositionMode = 1,
        ImmediateMode = 2
    }
}
