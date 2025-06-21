using System.Text.Json;
using LicenseManager.Converters;
using LicenseManager.Models.Responses;

namespace LicenseManager;

public class LicenseManager
{
    private string BaseUrl;
    private string ConsumerKey;
    private string ConsumerSecret;
    private string LicenseKey = string.Empty;
    private bool LicenseIsValid;
    
    public LicenseManager(string baseUrl, string consumerKey, string consumerSecret)
    {
        BaseUrl = baseUrl;
        ConsumerKey = consumerKey;
        ConsumerSecret = consumerSecret;
    }
    
    public async Task<bool> SetLicenseKey(string licenseKey)
    {
        LicenseKey = licenseKey;
        
        var licenseResponse = await GetLicense();
        if (licenseResponse.data.expiresAt == null)
        {
            LicenseIsValid = true;
            return true;
        }
        
        LicenseIsValid = licenseResponse.data.expiresAt > DateTime.UtcNow;
        return LicenseIsValid;
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

    public bool IsValid() => LicenseIsValid;
}