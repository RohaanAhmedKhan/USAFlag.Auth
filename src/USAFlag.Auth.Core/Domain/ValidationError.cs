namespace USAFlag.Auth.Core.Domain;

public class ValidationError
{
    public string propertyName { get; set; }
    public string errorMessage { get; set; }
    public string errorCode { get; set; }
}