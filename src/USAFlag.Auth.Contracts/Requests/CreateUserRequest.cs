namespace USAFlag.Auth.Contracts.Requests;

public class CreateUserRequest
{
    public int? userId { get; set; }
    public string? userName { get; set; }
    public string? password { get; set; }
}
