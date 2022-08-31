using System.Net.Http;
using System.Threading.Tasks;

namespace Kzrnm.MvvmSample.Wpf.Models.Services
{
    public interface IWebTimeService
    {
        Task<WorldClock> GetTimeAsync();
    }
    public class WebTimeService : IWebTimeService
    {
        private readonly HttpClient http;
        public WebTimeService(HttpClient http)
        {
            this.http = http;
        }
        public async Task<WorldClock> GetTimeAsync()
        {
            // {
            //     "$id": "1",
            //     "currentDateTime": "2021-12-13T15:30Z",
            //     "utcOffset": "00:00:00",
            //     "isDayLightSavingsTime": false,
            //     "dayOfTheWeek": "Monday",
            //     "timeZoneName": "UTC",
            //     "currentFileTime": 132838830284824910,
            //     "ordinalDate": "2021-347",
            //     "serviceResponse": null
            // }
            var url = "http://worldclockapi.com/api/json/utc/now";
            var response = await http.GetAsync(url).ConfigureAwait(false);
            using var stream = await response.Content.ReadAsStreamAsync();
            return await System.Text.Json.JsonSerializer.DeserializeAsync<WorldClock>(stream).ConfigureAwait(false);
        }
    }
}
