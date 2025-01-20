using AdventOfCode.Years;
using Microsoft.Extensions.Logging;

namespace AdventOfCode._2015;
internal class Year2015Day1Solution : IDaySolution
{
    public Year2015Day1Solution()
    {
    }

    public string GetFirstPartAnswer(string inputData)
    {
        return (inputData.Count(ch => ch == '(') - inputData.Count(ch => ch == ')')).ToString();
    }

    public string GetSecondPartAnswer(string inputData)
    {
        var currentFloor = 0;

        for (int i = 0; i < inputData.Length; i++)
        {
            if (inputData[i] == '(')
            {
                currentFloor++;
            }
            else
            {
                currentFloor--;
            }

            if (currentFloor == -1)
            {
                return (i + 1).ToString();
            }
        }

        throw new Exception("Basement floor not found");
    }
}
