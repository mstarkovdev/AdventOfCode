using AdventOfCode.Configuration;
using Microsoft.Extensions.Logging;

namespace AdventOfCode;
internal class InputDataFetcher
{
    private readonly ILogger<InputDataFetcher> _logger;
    private readonly InputDataConfiguration _inputDataConfiguration;

    private string _inputData = string.Empty;

    private const string INPUT_DATA_URL = "https://adventofcode.com/{0}/day/{1}/input";

    public InputDataFetcher(ILogger<InputDataFetcher> logger, 
        InputDataConfiguration inputDataConfiguration)
    {
        _logger = logger;
        _inputDataConfiguration = inputDataConfiguration;
    }

    public async Task<string> FetchAsync(int year, int day)
    {
        _logger.LogDebug("Fetching input data from year {0}, day {1}", year, day);

        if (!string.IsNullOrEmpty(_inputData))
        {
            return _inputData;
        }

        var pathToYearSolutionsFolder = Path.Combine(_inputDataConfiguration.PathToInputDataFiles, 
            year.ToString());
        var pathToYearDayInputDataFile = Path.Combine(pathToYearSolutionsFolder, $"{day}.txt");

        if (File.Exists(pathToYearDayInputDataFile))
        {
            _inputData = File.ReadAllText(pathToYearDayInputDataFile);
            return _inputData;
        }

        Directory.CreateDirectory(pathToYearSolutionsFolder);

        _inputData = await GetInputDataByUrl(year, day, pathToYearDayInputDataFile);
        return _inputData;
    }

    private async Task<string> GetInputDataByUrl(int year, int day, string pathToYearDayInputDataFile)
    {
        var url = string.Format(INPUT_DATA_URL, year, day);

        _logger.LogInformation("Fetching input data from URL: {0}", url);

        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Add("Cookie", $"session={_inputDataConfiguration.AuthroizationSessionCookie}");

            try
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var inputData = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("Input data fetched successfully");

                File.WriteAllText(pathToYearDayInputDataFile, inputData.Trim());

                return inputData;
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "Error fetching input data from URL: {0}", url);
                throw;
            }
        }
    }
}
