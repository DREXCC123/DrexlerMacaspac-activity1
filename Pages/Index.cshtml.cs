using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DrexlerMacaspac.Models;

namespace DrexlerMacaspac.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public List<PopulationRecord> PopulationRecords { get; set; } = new List<PopulationRecord>();

        public async Task OnGetAsync()
        {
            using var client = new HttpClient();

            try
            {
                client.BaseAddress = new Uri("https://datausa.io/");
                client.DefaultRequestHeaders.Add("User-Agent", "YourAppName/1.0");

                var response = await client.GetAsync("api/data?drilldowns=Nation&measures=Population");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var populationData = JsonConvert.DeserializeObject<PopulationData>(content);
                    PopulationRecords = populationData?.Data ?? new List<PopulationRecord>();
                }
                else
                {
                    _logger.LogError("Failed to fetch population data. Status code: {StatusCode}", response.StatusCode);
                }
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "Error fetching population data.");
            }
        }
    }
}
