namespace USAFlag.Auth.Core.Domain.Entities;

public class StoredProcedureResponse
{
    public int ReturnValue { get; set; }
    public int NewId { get; set; }
    public string OtherData { get; set; }
}
