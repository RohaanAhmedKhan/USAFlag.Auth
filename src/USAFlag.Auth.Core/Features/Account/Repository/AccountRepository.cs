namespace USAFlag.Auth.Core.Features.Account.Repository;

public interface IAccountRepository
{
    Task<StoredProcedureResponse> CreateUser(CreateUserRequest request, CancellationToken cancellationToken);
}

public class AccountRepository(DbContext dbContext) : IAccountRepository, IBaseRepository
{
    public async Task<StoredProcedureResponse> CreateUser(CreateUserRequest request, CancellationToken cancellationToken)
    {
        StoredProcedureResponse spResponse = new();

        const string storedProcedure = "auth.sp_create_user";
        var parameters = new DynamicParameters();
        parameters.Add("emailaddress", request?.emailAddress, DbType.String);
        parameters.Add("password", request?.password, DbType.String);
        parameters.Add("username", request?.userName, DbType.String);

        spResponse.ReturnValue = await dbContext.ExecuteStoredProcedureAsync<int>(storedProcedure, parameters, cancellationToken);

        return spResponse;
    }


}
