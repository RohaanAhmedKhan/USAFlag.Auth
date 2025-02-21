namespace USAFlag.Auth.Core.Domain;

public class JWTSettings
{
    public string issuer { get; set; }
    public string accessTokenExpireMinutes { get; set; }
    public string refreshTokenExpireDays { get; set; }
    public string jwtSecretKey { get; set; }
}
