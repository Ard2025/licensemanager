namespace LicenseManager.Models.Responses;

public class LicenseResponse : Response
{
    public int? id { get; set; }
    public int? orderId { get; set; }
    public int? productId { get; set; }
    public int? userId { get; set; }
    public string? licenseKey { get; set; }
    public string? expiresAt { get; set; }
    public int? validFor { get; set; }
    public int? source { get; set; }
    public int? status { get; set; }
    public int? timesActivated { get; set; }
    public int? timesActivatedMax { get; set; }
    public string? createdAt { get; set; }
    public int? createdBy { get; set; }
    public string? updatedAt { get; set; }
    public int? updatedBy { get; set; }
    public List<ActivationResponse>? activationData { get; set; } 
}