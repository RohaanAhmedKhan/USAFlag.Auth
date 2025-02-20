namespace USAFlag.Auth.API.Controllers.v1;


public abstract class BaseApiController : ControllerBase
{
    private readonly IConfiguration config;
    public int? UserId { get; set; }
    public int? TenantId { get; set; }
    public string TenantCode { get; set; }
    public string AppCode { get; set; }
    public int? RoleId { get; set; }

    public BaseApiController(IConfiguration config)
    {
        this.config = config;
    }

    [NonAction]
    public void ResolveUserIdentity()
    {
        if (User != null && User.Identity != null)
        {
            UserId = User.Identity.GetUserId();
            TenantId = User.Identity.GetTenantId();
            RoleId = User.Identity.GetUserRoleId();
        }
    }

    [NonAction]
    protected ApiResponse BuildValidationErrorApiResponse(List<ValidationError> validationErrors)
    {
        return new ApiResponse
        {
            Response = validationErrors,
            ErrorMessage = "Validation Errors",
            StatusCode = 422,
            Status = false,
            Message = "Failed"
        };
    }

    [NonAction]
    protected ApiResponse BuildValidationErrorApiResponse(FluentValidation.Results.ValidationResult validationResult)
    {
        var validationErrors = new List<ValidationError>();
        var errors = validationResult.Errors;
        foreach (var error in errors)
        {
            validationErrors.Add(new ValidationError
            {
                ErrorCode = error.ErrorCode,
                ErrorMessage = error.ErrorMessage,
                PropertyName = error.PropertyName
            });
        }

        return new ApiResponse
        {
            Response = validationErrors,
            ErrorMessage = "Validation Errors",
            StatusCode = 422,
            Status = false,
            Message = "Failed"
        };
    }

    [NonAction]
    protected string Encrypt(string plainTextPassword)
    {
        string aeskey = config.GetValue<string>("AESSecret");
        return Convert.ToBase64String(SuiteB.Encrypt(plainTextPassword.ToBytes(),
            aeskey.ToBytes()));
    }


    [NonAction]
    protected string Decrypt(string encryptedText)
    {
        if (string.IsNullOrEmpty(encryptedText))
            return null;

        string aeskey = config.GetValue<string>("AESSecret");
        return Convert.ToBase64String(SuiteB.Decrypt(encryptedText.ToBytes(),
            aeskey.ToBytes()));
    }

    [NonAction]
    protected ApiResponse CreateSuccessApiResponse(object response,
        HttpStatusCode statusCode = HttpStatusCode.OK, string message = "Success")
    {
        return new ApiResponse
        {
            Response = response,
            ErrorMessage = "",
            StatusCode = (int)statusCode,
            Status = true,
            Message = message
        };
    }

    [NonAction]
    protected ApiResponse CreateFailedApiResponse(object response,
        HttpStatusCode statusCode, string errorMessage)
    {
        return new ApiResponse
        {
            Response = response,
            ErrorMessage = errorMessage,
            StatusCode = (int)statusCode,
            Status = false,
            Message = "Failed"
        };
    }
}