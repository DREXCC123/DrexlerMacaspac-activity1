using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace DrexlerMacaspac.Pages
{
    public class Index : PageModel
    {
        private readonly ILogger<Index> _logger;

        public Index(ILogger<Index> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public Result? DataResult { get; set; }



        public async Task<IActionResult> OnGet()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync("https://datausa.io/api/data?drilldowns=Nation&measures=Population");

            if (response.IsSuccessStatusCode)
            {
                string? content = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(content))
                {
                    Result? result = JsonConvert.DeserializeObject<Result>(content);

                    if (result != null)
                    {
                        this.DataResult = result;
                    }

                }
            }

            return Page();
        }

        public class Result
        {
            [JsonProperty("data")]
            public List<PopulationData> Data { get; set; }

            [JsonProperty("source")]
            public List<Source> Source { get; set; }
        }

        public class PopulationData
        {
            [JsonProperty("ID Nation")]
            public string IDNation { get; set; }

            [JsonProperty("Nation")]
            public string Nation { get; set; }

            [JsonProperty("ID Year")]
            public int IDYear { get; set; }

            [JsonProperty("Year")]
            public string Year { get; set; }

            [JsonProperty("Population")]
            public int Population { get; set; }

            [JsonProperty("Slug Nation")]
            public string SlugNation { get; set; }
        }

        public class Source
        {
            [JsonProperty("measures")]
            public List<string> Measures { get; set; }

            [JsonProperty("annotations")]
            public Annotations Annotations { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("substitutions")]
            public List<object> Substitutions { get; set; }
        }

        public class Annotations
        {
            [JsonProperty("source_name")]
            public string SourceName { get; set; }

            [JsonProperty("source_description")]
            public string SourceDescription { get; set; }

            [JsonProperty("dataset_name")]
            public string DatasetName { get; set; }

            [JsonProperty("dataset_link")]
            public string DatasetLink { get; set; }

            [JsonProperty("table_id")]
            public string TableId { get; set; }

            [JsonProperty("topic")]
            public string Topic { get; set; }

            [JsonProperty("subtopic")]
            public string Subtopic { get; set; }
        }
    }
}