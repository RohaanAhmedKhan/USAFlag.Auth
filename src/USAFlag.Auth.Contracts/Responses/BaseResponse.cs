namespace USAFlag.Auth.Contracts.Response;

public class BaseResponse
{ 
    public int createdBy { get; set; }
    public DateTime createdDate { get; set; }
    public int tenantId { get; set; }
    public int? modifiedBy { get; set; }
    public DateTime? modifiedDate { get; set; }
    public bool active { get; set; }
}
