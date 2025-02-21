using Dapper;

namespace USAFlag.Auth.Core.Features.Account.Repository;

public interface IAccountRepository
{
    Task<StoredProcedureResponse> CreateUser(CreateUserRequest request, CancellationToken cancellationToken);
}

public class AccountRepository(DbContext dbContext) : IAccountRepository, IBaseRepository
{
    public async Task<StoredProcedureResponse> CreateUser(CreateUserRequest request, CancellationToken cancellationToken)
    {
        const string procedure = $"CALL auth.sp_create_user(@userName, @emailAddress, @password, @result);";

        var parameters = new DynamicParameters();
        parameters.Add("emailaddress", "test1@example.com", DbType.String);
        parameters.Add("password", "secuaare_password", DbType.String);
        parameters.Add("username", "tesaat_user", DbType.String);

        // Call the procedure
        int result = await dbContext.ExecuteStoredProcedureAsync<int>("auth.sp_create_user", parameters);

        return new StoredProcedureResponse
        {
            NewId = 123,
            OtherData = null,
            ReturnValue = result
        };
    }


    // Call the stored procedure to get a user by ID and return the user object
    //public async Task<User> GetUserByIdAsync(int id)
    //{
    //    const string procedureName = "get_user_by_id";
    //    return await _dbContext.ExecuteStoredProcedureAsync<User>(procedureName, new { UserId = id });
    //}
}
