using System.Text.Json;
using LicenseManager.Converters;
using LicenseManager.Models.Responses;

namespace LicenseManager;

public class LicenseManager
{
    private string BaseUrl;
    private string ConsumerKey;
    private string ConsumerSecret;
    private string LicenseKey;
    
    public LicenseManager(string baseUrl, string consumerKey, string consumerSecret, string licenseKey)
    {
        BaseUrl = baseUrl;
        ConsumerKey = consumerKey;
        ConsumerSecret = consumerSecret;
        LicenseKey = licenseKey;
    }

    public void setLicenseKey(string licenseKey)
    {
        ConsumerKey = licenseKey;
    }

    private async Task<string> CallApi(string route)
    {
        HttpClient client = new HttpClient();
        return await client.GetStringAsync($"{BaseUrl}{route}?consumer_key={ConsumerKey}&consumer_secret={ConsumerSecret}");
    }

    public async Task<ApiResponse<LicenseResponse>?> GetLicense()
    {
        var response = await CallApi($"/wp-json/lmfwc/v2/licenses/{LicenseKey}");
        return System.Text.Json.JsonSerializer.Deserialize<ApiResponse<LicenseResponse>>(response, new JsonSerializerOptions()
        {
            Converters = { new DateTimeApiConverter() }
        });
    }

    public async Task<bool> LicenseIsValid()
    {
        var licenseResponse = await GetLicense();
        if (licenseResponse.data.expiresAt == null)
        {
            return true;
        }
        return licenseResponse.data.expiresAt > DateTime.UtcNow;
    }
}