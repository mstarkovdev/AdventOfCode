using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions._2015;
internal class Year2015Day8Solution : IDaySolution
{
    public string GetFirstPartAnswer(string inputData)
    {
        var literalCount = 0;
        var memoryChCount = 0;

        var lines = inputData.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            var lineLiteralCount = line.Length;

            var unescapedLine = Regex.Unescape(line.Replace("\\\"", "b"));
            unescapedLine = unescapedLine.Replace("\"", "");
            var lineMemoryChCount = unescapedLine.Length;

            literalCount += lineLiteralCount;
            memoryChCount += lineMemoryChCount;
        }

        return (literalCount - memoryChCount).ToString();
    }

    public string GetSecondPartAnswer(string inputData)
    {
        var literalCount = 0;
        var memoryChCount = 0;

        var escapedLiterals = new HashSet<char>
        {
            '\"',
            '\\'
        };

        var lines = inputData.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            var lineLiteralCount = line.Length;

            var escapedChCount = line.Count(escapedLiterals.Contains);
            var lineMemoryChCount = lineLiteralCount + escapedChCount + 2;

            literalCount += lineLiteralCount;
            memoryChCount += lineMemoryChCount;
        }

        return (memoryChCount - literalCount).ToString();
    }
}
