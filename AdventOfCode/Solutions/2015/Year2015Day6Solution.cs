using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions._2015;
internal class Year2015Day6Solution : IDaySolution
{
    public string GetFirstPartAnswer(string inputData)
    {
        var grid = new bool[1000, 1000];
        var instructions = inputData.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        foreach (var instruction in instructions)
        {
            var coordsMatches = Regex.Matches(instruction, @"\d+,\d+");

            var startCoords = coordsMatches[0].Value.Split(",").Select(int.Parse).ToArray();
            var endCoords = coordsMatches[1].Value.Split(",").Select(int.Parse).ToArray();

            for (int x = startCoords[0]; x <= endCoords[0]; x++)
            {
                for (int y = startCoords[1]; y <= endCoords[1]; y++)
                {
                    switch (instruction)
                    {
                        case var s when s.StartsWith("turn on"):
                            grid[x, y] = true;
                            break;
                        case var s when s.StartsWith("turn off"):
                            grid[x, y] = false;
                            break;
                        case var s when s.StartsWith("toggle"):
                            grid[x, y] = !grid[x, y];
                            break;
                    }
                }
            }
        }

        var lights = 0;
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i, j])
                {
                    lights++;
                }
            }
        }

        return lights.ToString();
    }

    public string GetSecondPartAnswer(string inputData)
    {
        var grid = new int[1000, 1000];
        var instructions = inputData.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        foreach (var instruction in instructions)
        {
            var coordsMatches = Regex.Matches(instruction, @"\d+,\d+");

            var startCoords = coordsMatches[0].Value.Split(",").Select(int.Parse).ToArray();
            var endCoords = coordsMatches[1].Value.Split(",").Select(int.Parse).ToArray();

            for (int x = startCoords[0]; x <= endCoords[0]; x++)
            {
                for (int y = startCoords[1]; y <= endCoords[1]; y++)
                {
                    if (instruction.StartsWith("turn on"))
                    {
                        grid[x, y]++;
                    }
                    else if (instruction.StartsWith("turn off"))
                    {
                        if (grid[x, y] > 0)
                        {
                            grid[x, y]--;
                        }
                    }
                    else
                    {
                        grid[x, y] += 2;

                    }
                }
            }
        }

        var brightness = 0;
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                brightness += grid[i, j];
            }
        }

        return brightness.ToString();
    }
}
