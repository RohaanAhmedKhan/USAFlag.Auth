namespace Grid.Auth.Domain;

public class JWTSettings
{
    public string Issuer { get; set; }
    public string AccessTokenExpireMinutes { get; set; }
    public string RefreshTokenExpireDays { get; set; }
    public string JWTSecretKey { get; set; }
}
