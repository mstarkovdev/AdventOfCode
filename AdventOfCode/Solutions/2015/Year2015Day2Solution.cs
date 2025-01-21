namespace AdventOfCode.Solutions._2015;
internal class Year2015Day2Solution : IDaySolution
{
    public string GetFirstPartAnswer(string inputData)
    {
        var boxDimensions = inputData.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        var totalSquareFeet = 0;

        foreach (var boxDimension in boxDimensions)
        {
            var sides = boxDimension.Split('x').Select(int.Parse).ToArray();

            var areas = new List<int>
            {
                sides[0] * sides[1],
                sides[1] * sides[2],
                sides[2] * sides[0]
            };

            totalSquareFeet += 2 * (areas[0] + areas[1] + areas[2]) + areas.Min();
        }

        return totalSquareFeet.ToString();
    }

    public string GetSecondPartAnswer(string inputData)
    {
        var boxDimensions = inputData.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        var totalRibbonFeet = 0;

        foreach (var boxDimension in boxDimensions)
        {
            var sides = boxDimension.Split('x').Select(int.Parse).Order().ToArray();

            totalRibbonFeet += 2 * (sides[0] + sides[1]) + sides[0] * sides[1] * sides[2];
        }

        return totalRibbonFeet.ToString();
    }
}
