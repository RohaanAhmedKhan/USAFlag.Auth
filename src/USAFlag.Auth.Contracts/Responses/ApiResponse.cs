namespace USAFlag.Auth.Contracts.Response;

public class ApiResponse
{
    public bool status { get; set; }
    public int statusCode { get; set; }
    public string? message { get; set; }
    public object? response { get; set; }
    public string? errorMessage { get; set; }
    public object? validationErrors { get; set; }
}
