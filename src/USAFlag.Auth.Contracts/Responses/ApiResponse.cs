namespace Grid.Auth.Service.Response;

public class ApiResponse
{
    public bool Status { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public object? Response { get; set; }
    public string? ErrorMessage { get; set; }
    public object? ValidationErrors { get; set; }
}
