using measles_tracker.app.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Caching.Memory;
using System.Globalization;

namespace measles_tracker.app.Services
{
    public class CSVDataService
    {
        private readonly HttpClient _http;

        public CSVDataService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<MeaslesData>> GetMeaslesDataAsync(bool forceRefresh = false)
        {
            //Raw GitHub CSV file
            var url = "https://raw.githubusercontent.com/CSSEGISandData/measles_data/refs/heads/main/measles_county_all_updates.csv";

            if (forceRefresh)
            {
                // Append a timestamp to force GitHub to bypass cached CDN response
                url += $"?t={DateTime.UtcNow.Ticks}";
            }

            var csvText = await _http.GetStringAsync(url);

            using var reader = new StringReader(csvText);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null,
                IgnoreBlankLines = true,
                HasHeaderRecord = true,
                Delimiter = ","
            };

            using var csv = new CsvReader(reader, config);
            var records = new List<MeaslesData>();

            while (await csv.ReadAsync())
            {
                try
                {
                    records.Add(csv.GetRecord<MeaslesData>());
                }
                catch { }
            }

            return records;
        }
    }

}
