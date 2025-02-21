namespace USAFlag.Auth.Contracts.Requests.Account;

public class CreateUserRequest
{
    public string? userName { get; set; }
    public string? emailAddress { get; set; }    
    public string? password { get; set; }

}
