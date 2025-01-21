using AdventOfCode.Configuration;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Solutions;
internal class PuzzleSolver
{
    private readonly ILogger<PuzzleSolver> _logger;
    private readonly InputDataFetcher _inputDataFetcher;
    private readonly IDaySolution _daySolution;
    private readonly CurrentPuzzleInfo _currentPuzzleInfo;

    public PuzzleSolver(ILogger<PuzzleSolver> logger, InputDataFetcher inputDataFetcher,
        IDaySolution daySolution, CurrentPuzzleInfo currentPuzzleInfo)
    {
        _logger = logger;
        _inputDataFetcher = inputDataFetcher;
        _daySolution = daySolution;
        _currentPuzzleInfo = currentPuzzleInfo;
    }

    public async Task SolveFirstPartAsync()
    {
        await SolvePartAsync(_currentPuzzleInfo.FirstPartTestCases,
            _daySolution.GetFirstPartAnswer, 1);
    }

    public async Task SolveSecondPartAsync()
    {
        await SolvePartAsync(_currentPuzzleInfo.SecondPartTestCases,
            _daySolution.GetSecondPartAnswer, 2);
    }

    private async Task SolvePartAsync(IEnumerable<TestCase> testCases, Func<string, string> getAnswer, int partNumber)
    {
        _logger.LogInformation("Solving part {0} of the puzzle", partNumber);

        var methodInfo = getAnswer.Method;
        if (methodInfo.GetCustomAttributes(typeof(NotImplementedAttribute), false).Length > 0)
        {
            _logger.LogInformation("Part {0} not yet implemented", partNumber);
            return;
        }

        if (testCases != null && testCases.Any(testCase => !string.IsNullOrEmpty(testCase.TestData)))
        {
            _logger.LogInformation("Running test cases");

            foreach (var testCase in testCases)
            {
                var testSolution = getAnswer(testCase.TestData);
                if (testSolution != testCase.ExpectedResult)
                {
                    _logger.LogError("Test case failed. Input data: {0}, expected result: \u001b[34;1m{1}\u001b[33;1m, actual result: \u001b[31m{2}",
                        testCase.TestData, testCase.ExpectedResult, testSolution);
                    return;
                }
            }

            _logger.LogInformation("All test cases passed successfully");
        }

        var inputData = await _inputDataFetcher.FetchAsync(_currentPuzzleInfo.Year, _currentPuzzleInfo.Day);
        var solution = getAnswer(inputData);

        _logger.LogInformation("Solution to the part {0} is \u001b[34;1m{1}", partNumber, solution);
    }
}
