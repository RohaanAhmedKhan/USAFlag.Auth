namespace Grid.Auth.Service.Response;

public class BaseResponse
{ 
    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public int TenantId { get; set; }
    public int? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool Active { get; set; }
}
