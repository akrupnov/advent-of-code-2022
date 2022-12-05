namespace Solutions
{
    public abstract class AbstractSolution
    {        
        public AbstractSolution(String input)
        {
            Input = input;
        }
        public String Input { get; }

        public abstract String SolvePartOne();
        public abstract String SolvePartTwo();
    }
}