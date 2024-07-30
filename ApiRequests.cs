
using System.Text;
using System.Text.Json;

class ApiRequests
{
    private string apikey;
    private string username;
    private string password;
    private string token;
    private HttpClient client;
    public ApiRequests(string Apikey, string Username, string Password) {
        apikey = Apikey;
        username = Username;
        password = Password;
        client = ClientSetup();
    }
    public static string BaseAPIUrl = "https://api.opensubtitles.com/api/v1";
    
    private  HttpClient ClientSetup()
    {
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("Api-Key", $"{apikey}");
        client.DefaultRequestHeaders.Add("User-Agent", "1234");
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        client.DefaultRequestHeaders.Add("Prefer", "code=200, example=example-1");
        return client;
    }

    public async Task<LoginInfo> Login()
    {
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{BaseAPIUrl}/login");
        var jsonObj = new
        {
            username,
            password
        };
        requestMessage.Content = new StringContent(JsonSerializer.Serialize(jsonObj), Encoding.UTF8, "application/json");
        requestMessage.Headers.Add("Prefer", "code=200");
        var response = await client.SendAsync(requestMessage);
        string responseAsString = await response.Content.ReadAsStringAsync();
        LoginInfo logininfo = JsonSerializer.Deserialize<LoginInfo>(responseAsString);
        token = logininfo.token;
        return logininfo;
    }
    public async Task<LogoutInfo> Logout(string token = "string")
    {
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{BaseAPIUrl}/logout");
        requestMessage.Headers.Add("Prefer", "code=200, example=default");
        requestMessage.Headers.Add("Authorization", $"Bearer {token}");
        var response = await client.SendAsync(requestMessage);
        string responseAsString = await response.Content.ReadAsStringAsync();
        LogoutInfo logoutinfo = JsonSerializer.Deserialize<LogoutInfo>(responseAsString);
        return logoutinfo;
    }
    public async Task GetRecentList()
    {

        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{BaseAPIUrl}/discover/latest");
        requestMessage.Headers.Add("Prefer", "code=200, example=default");
        requestMessage.Headers.Add("Authorization", $"Bearer {token}");
        var response = await client.SendAsync(requestMessage);
        string responseAsString = await response.Content.ReadAsStringAsync();
    }

    public async Task<SubtitleResults> SearchSubtitle(string searchterm)
    {
        Console.WriteLine(token);
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{BaseAPIUrl}/subtitles?query={searchterm}");
        requestMessage.Headers.Add("Authorization", $"Bearer {token}");
        var response = await client.SendAsync(requestMessage);
        
        string responseAsString = await response.Content.ReadAsStringAsync();
        var x = JsonSerializer.Deserialize<SubtitleResults>(responseAsString);
        return x; 
    }

    class FileIDRequest 
    {
        public int file_id {set; get;}
    }

    public async Task<DownLoadLinkData> RequestDownloadURL(int subid)
    {
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{BaseAPIUrl}/download");
        requestMessage.Headers.Add("Authorization", $"Bearer {token}");
        string s = $"{{\n  \"file_id\": {subid}\n}}";
        requestMessage.Content = new StringContent(s, Encoding.ASCII, "application/json");
        
        var response = await client.SendAsync(requestMessage);
        string responseAsString = await response.Content.ReadAsStringAsync();
        var x = JsonSerializer.Deserialize<DownLoadLinkData>(responseAsString);
        return x;
    }

    public async Task DownloadSubFile(DownLoadLinkData info)
    {
        Stream fileStream = await client.GetStreamAsync(info.link);

        FileStream outputFileStream = new FileStream($".\\{info.file_name}.sub", FileMode.CreateNew);
        await fileStream.CopyToAsync(outputFileStream);
        // return fileStream;
    }

}

