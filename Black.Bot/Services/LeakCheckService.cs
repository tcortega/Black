using Black.Bot.DTOs;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Black.Bot.Services
{
    public class LeakCheckService
    {
        private readonly HttpClient _httpClient;
        private readonly string _leakCheckKeyApiKey;
        private string _baseUrl = "https://leakcheck.net/api/";

        public LeakCheckService(HttpClient httpClient, IOptions<BlackBotSettings> options)
        {
            _httpClient = httpClient;
            _leakCheckKeyApiKey = options.Value.LeakCheckApiKey;
        }

        private string GetQueryUrl(string data, string queryType)
        {
            return $"{_baseUrl}?key={_leakCheckKeyApiKey}&check={data}&type={queryType}";
        }


        public async Task<LeakCheckResult> GetResults(string data, string queryType = "auto")
        {
            return await _httpClient.GetFromJsonAsync<LeakCheckResult>(GetQueryUrl(data, queryType));
        }
    }
}
