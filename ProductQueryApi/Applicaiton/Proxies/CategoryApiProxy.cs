using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Applicaiton.Interaces;
using Applicaiton.Responces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Applicaiton.Proxies
{
    public class CategoryApiProxy : ICategoryApiProxy
    {
        public CategoryResponse categoryResponse { get; set; }
        
        private readonly HttpClient _httpClient; 
        private readonly ILogger<CategoryApiProxy> _logger;

        public CategoryApiProxy(
            HttpClient httpClient,
            ILogger<CategoryApiProxy> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
           
        }
        public async Task<CategoryResponse> GetCategoryById(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{id}");

                if (response.IsSuccessStatusCode)
                {
                    var responseStream = await response.Content.ReadAsStreamAsync();
                    StreamReader reader = new StreamReader(responseStream);
                    string text = reader.ReadToEnd();
                    categoryResponse = JsonConvert.DeserializeObject<CategoryResponse>(text);
                }
                else
                {
                    _logger.LogError("Error : category api url or something");
                }
            }
            catch (Exception ex)
            {
                 _logger.LogInformation(ex.Message);
            }
            return categoryResponse;
        }
    }
}