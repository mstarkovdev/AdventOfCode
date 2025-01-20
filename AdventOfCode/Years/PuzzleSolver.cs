using AdventOfCode.Configuration;
using Microsoft.Extensions.Logging;

namespace AdventOfCode.Years;
internal class PuzzleSolver
{
    private readonly ILogger<PuzzleSolver> _logger;
    private readonly InputDataFetcher _inputDataFetcher;
    private readonly IDaySolution _daySolution;
    private readonly CurrentPuzzleInfo _currentPuzzleInfo;

    private string _inputData;

    public PuzzleSolver(ILogger<PuzzleSolver> logger, InputDataFetcher inputDataFetcher, 
        IDaySolution daySolution, CurrentPuzzleInfo currentPuzzleInfo)
    {
        _logger = logger;
        _inputDataFetcher = inputDataFetcher;
        _daySolution = daySolution;
        _currentPuzzleInfo = currentPuzzleInfo;

        _inputData = string.Empty;
    }

    public async Task SolveFirstPartAsync()
    {
        _logger.LogInformation("Solving first part of the puzzle");

        var inputData = await GetInputDataAsync();
        var solution = _daySolution.GetFirstPartAnswer(_inputData);

        _logger.LogInformation("Solution to the first part is {0}", solution);
    }

    public async Task SolveSecondPartAsync()
    {
        _logger.LogInformation("Solving second part of the puzzle");

        var inputData = await GetInputDataAsync();
        var solution = _daySolution.GetSecondPartAnswer(_inputData);

        _logger.LogInformation("Solution to the second part is {0}", solution);
    }

    private async Task<string> GetInputDataAsync()
    {
        if (string.IsNullOrEmpty(_inputData))
        {
            _inputData = await _inputDataFetcher.FetchAsync(_currentPuzzleInfo.Year, _currentPuzzleInfo.Day);
        }
        return _inputData;
    }
}
