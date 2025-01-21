using System.Security.Cryptography;

namespace AdventOfCode.Solutions._2015;
internal class Year2015Day4Solution : IDaySolution
{
    public string GetFirstPartAnswer(string inputData)
    {
        return GetSolutionHash(inputData, "00000");
    }

    public string GetSecondPartAnswer(string inputData)
    {
        return GetSolutionHash(inputData, "000000");
    }

    private string GetSolutionHash(string inputData, string exitCondition)
    {
        var hash = string.Empty;
        var numbers = 0;

        while (!hash.StartsWith(exitCondition))
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = System.Text.Encoding.ASCII.GetBytes($"{inputData}{numbers}");
                var hashBytes = md5.ComputeHash(inputBytes);
                hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty).ToLower();
            }

            numbers++;
        }

        return (numbers - 1).ToString();
    }
}
