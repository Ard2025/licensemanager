using System.Text.Json;
using LicenseManager.Converters;
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

    private async Task<string> CallApi(string route)
    {
        HttpClient client = new HttpClient();
        return await client.GetStringAsync($"{BaseUrl}{route}?consumer_key={ConsumerKey}&consumer_secret={ConsumerSecret}");
    }

    public async Task<ApiResponse<LicenseResponse>?> GetLicense(string licenseKey)
    {
        var response = await CallApi($"/wp-json/lmfwc/v2/licenses/{licenseKey}");
        return System.Text.Json.JsonSerializer.Deserialize<ApiResponse<LicenseResponse>>(response, new JsonSerializerOptions()
        {
            Converters = { new DateTimeApiConverter() }
        });
    }

    public async Task<bool> LicenseIsValid(string licenseKey)
    {
        var licenseResponse = await GetLicense(licenseKey);
        if (licenseResponse.data.expiresAt == null)
        {
            return true;
        }
        return licenseResponse.data.expiresAt > DateTime.UtcNow;
    }
}