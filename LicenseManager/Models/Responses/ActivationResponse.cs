namespace LicenseManager.Models.Responses;

public class ActivationResponse: Response
{
    public int? id { get; set;}
    public string? token { get; set;}
    public int? license_id { get; set;}
    public string? label { get; set;}
    public int? source { get; set;}
    public string? ip_address { get; set;}
    public string? user_agent { get; set;}
    public string? meta_data { get; set;}
    public string? created_at { get; set;}
    public string? updated_at { get; set;}
    public string? deactivated_at { get; set;}
}