using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GitHubRepos.API.Controllers
{
    public class GitHubController : Controller
    {
        private readonly GitHubClient _gitHubClient;

        public GitHubController(GitHubClient gitHubClient)
        {
            _gitHubClient = gitHubClient;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetRepositories(string username)
        {
            var repositories = await _gitHubClient.GetRepositoriesAsync(username);

            return Ok(repositories);
        }
    }
}

