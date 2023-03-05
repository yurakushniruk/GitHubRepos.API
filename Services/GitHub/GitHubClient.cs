using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace GitHubRepos
{
    public class GitHubClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _userAgent;

        public GitHubClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = Configuration.GetSection("GitHub:BaseUrl").Value;
            _userAgent = Configuration.GetSection("GitHub:UserAgent").Value;
        }

        public async Task<List<Repository>> GetRepositoriesAsync(string username)
        {
            var url = $"{_baseUrl}users/{username}/repos";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.UserAgent.Add(new ProductInfoHeaderValue(_userAgent, "1.0"));

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var repositories = JsonConvert.DeserializeObject<List<Repository>>(content);

            return repositories;
        }
    }
}
