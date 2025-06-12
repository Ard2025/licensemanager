namespace LicenseManager.Models.Responses;

public class ApiResponse<T> where T : Response
{
    public bool success { get; set; }
    public T? data { get; set; }
}

public class ApiResponseArray<T> where T : Response
{
    public bool success { get; set; }
    public List<T>? data { get; set; }
}

public class Response;