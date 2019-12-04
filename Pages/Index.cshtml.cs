using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace cross_plat_docker.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task OnGet()
        {
            var client = _httpClientFactory.CreateClient();
            var url = _config.GetValue("FetchUrl","https://www.bbc.co.uk/");
            ViewData["Url"] = url;
            try
            {
                var message = await client.GetStringAsync(url);
                ViewData["Message"] = message.Length > 4000 ? message.Substring(0, 4000) : message;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"couldn't download {url}");
                ViewData["Message"] = e.Message;
            }
        }

        public async Task<IActionResult> OnGetData()
        {
            return new JsonResult(new[] { "Hello world", Environment.OSVersion.VersionString });
        }
    }
}
