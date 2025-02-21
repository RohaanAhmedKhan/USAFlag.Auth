namespace USAFlag.Auth.API.Controllers.v1;


public abstract class BaseApiController : ControllerBase
{
    private readonly IConfiguration config;
    public int? userId { get; set; }
    public int? tenantId { get; set; }
    public string tenantCode { get; set; }
    public string appCode { get; set; }
    public int? roleId { get; set; }

    public BaseApiController(IConfiguration config)
    {
        this.config = config;
    }

    [NonAction]
    public void ResolveUserIdentity()
    {
        if (User != null && User.Identity != null)
        {
            this.userId = User.Identity.GetUserId();
            this.tenantId = User.Identity.GetTenantId();
            this.roleId = User.Identity.GetUserRoleId();
        }
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

}
