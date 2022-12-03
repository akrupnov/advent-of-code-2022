namespace Solutions;
using System.Linq;
public class DayOne
{
    public String Solve(String input)
    {
        var maxElveCalories = input.Split("\n\n").Select(x => x.Split("\n").Select(s => Convert.ToInt32(s))).Max(e => e.Sum());

        return maxElveCalories.ToString();
    }
}
