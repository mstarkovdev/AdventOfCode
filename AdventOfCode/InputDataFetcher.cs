using AdventOfCode.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode;
internal class InputDataFetcher
{
    private readonly ILogger<InputDataFetcher> _logger;
    private readonly AuthorizationConfiguration _authorizationConfiguration;

    private const string INPUT_DATA_URL = "https://adventofcode.com/{0}/day/{1}/input";

    public InputDataFetcher(ILogger<InputDataFetcher> logger, 
        AuthorizationConfiguration authorizationConfiguration)
    {
        _logger = logger;
        _authorizationConfiguration = authorizationConfiguration;
    }

    public async Task<string> FetchAsync(int year, int day)
    {
        _logger.LogDebug("Fetching input data from year {0}, day {1}", year, day);

        var url = string.Format(INPUT_DATA_URL, year, day);

        _logger.LogDebug("Fetching input data from URL: {0}", url);

        var inputData = string.Empty;

        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Add("Cookie", $"session={_authorizationConfiguration.SessionCookie}");

            try
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                inputData = await response.Content.ReadAsStringAsync();
                _logger.LogDebug("Input data fetched successfully");
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "Error fetching input data from URL: {0}", url);
            }
        }

        return inputData;
    }
}
