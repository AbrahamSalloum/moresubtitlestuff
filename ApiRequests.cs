
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;




    class ApiRequests
    {
        static HttpClient client = new HttpClient();

        public static async Task GetRecentList()
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://stoplight.io/mocks/opensubtitles/opensubtitles-api/2781383/discover/latest");
            requestMessage.Headers.Add("Api-Key", "123");
            requestMessage.Headers.Add("Prefer", "code=200, dynamic=true");
            requestMessage.Headers.Add("User-Agent", "*");
            requestMessage.Headers.Add("Accept", "application/json");

            var response = await client.SendAsync(requestMessage);
            string responseAsString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseAsString);
        }

        public static async Task SearchSubtitle(string searchterm = "test")
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, $"https://stoplight.io/mocks/opensubtitles/opensubtitles-api/2781383/subtitles?query={searchterm}");
            requestMessage.Headers.Add("Api-Key", "123");
            requestMessage.Headers.Add("Prefer", "code=200, example=example-1");
            requestMessage.Headers.Add("User-Agent", "*");
            requestMessage.Headers.Add("Accept", "application/json");

            var response = await client.SendAsync(requestMessage);
            string responseAsString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseAsString);
        }

        public static async Task DownloadSubtitle(int subid = 123)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, $"https://stoplight.io/mocks/opensubtitles/opensubtitles-api/2781383/download?query={subid}");
            requestMessage.Headers.Add("Api-Key", "123");
            requestMessage.Headers.Add("Prefer", "code=200, example=example-1");
            requestMessage.Headers.Add("User-Agent", "*");
            requestMessage.Headers.Add("Accept", "application/json");
            var jsonObj = new
            {
                file_id = subid,
            };
            requestMessage.Content = new StringContent(JsonSerializer.Serialize(jsonObj), Encoding.UTF8, "application/json");
            var response = await client.SendAsync(requestMessage);
            string responseAsString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseAsString);
        }


    }
