namespace Grid.Auth.Domain;

public class ValidationError
{
    public string PropertyName { get; set; }
    public string ErrorMessage { get; set; }
    public string ErrorCode { get; set; }
}