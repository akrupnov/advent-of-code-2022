namespace Solutions;
using System.Linq;
public class DayOne
{
    private readonly string _input;

    public DayOne(String input)
    {
        this._input = input;
    }
    public String SolvePartOne()
    {
        var maxElveCalories = GetCalorieValues().Max(e => e.Sum());

        return maxElveCalories.ToString();
    }

    public String SolvePartTwo()
    {
        return GetCalorieValues().Select(e => e.Sum()).OrderDescending().Take(3).Sum().ToString();
    }

    private IEnumerable<IEnumerable<int>> GetCalorieValues()
    {
        return _input.Split("\n\n").Select(x => x.Split("\n").Select(s => Convert.ToInt32(s)));
    }
}
