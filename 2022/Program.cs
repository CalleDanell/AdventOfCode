using _2022.Days;
using Common;

var days = new List<IDay>
    {
        new Day01()
    };

var solver = new Solver();
solver.AddProblems(days);
await solver.SolveAll();