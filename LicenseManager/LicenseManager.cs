using LicenseManager.Models.Responses;

namespace LicenseManager;

public class LicenseManager
{
    private string BaseUrl;
    private string ConsumerKey;
    private string ConsumerSecret;
    
    public LicenseManager(string baseUrl, string consumerKey, string consumerSecret)
    {
        BaseUrl = baseUrl;
        ConsumerKey = consumerKey;
        ConsumerSecret = consumerSecret;
    }

    public async Task<ApiResponse<LicenseResponse>?> GetLicense(string licenseKey)
    {
        HttpClient client = new HttpClient();
        var response = await client.GetStringAsync($"{BaseUrl}/wp-json/lmfwc/v2/licenses/{licenseKey}?consumer_key={ConsumerKey}&consumer_secret={ConsumerSecret}");
        return System.Text.Json.JsonSerializer.Deserialize<ApiResponse<LicenseResponse>>(response);
    }
}