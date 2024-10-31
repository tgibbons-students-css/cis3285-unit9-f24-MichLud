using SingleResponsibilityPrinciple.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class RestfulTradeDataProvider : ITradeDataProvider
    {
        string url;
        ILogger logger;
        public RestfulTradeDataProvider(string url, ILogger logger)
        {
            this.url = url;
            this.logger = logger;
        }

        public IEnumerable<string> GetTradeData()
        {
            List<string> tradeData = new List<string>();

            // Use HttpCLient and GetAsync to connect to the server
            using (var client = new HttpClient()) {
                try {
                    HttpResponseMessage response = client.GetAsync(url).Result;

                    response.EnsureSuccessStatusCode();

                    string jsonData = response.Content.ReadAsStringAsync().Result;

                    List<string> trades = JsonSerializer.Deserialize<List<string>>(jsonData);

                    if (trades != null) {
                        tradeData.AddRange(trades);
                    }
                }
                catch (HttpRequestException e) {
                    logger.LogWarning($"Failed to retrieve data. Status code: {e.Message}");
                }

            // Use ReadAsStringAsync() to read data from
            // Use JSONSerializer to convert the Json data into a trade string.
            }
            return tradeData;
        }
    }
}
