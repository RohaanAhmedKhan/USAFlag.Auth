namespace USAFlag.Auth.Core.Shared.Services.Account;

public interface IAccountService
{
     Task<StoredProcedureResponse> CreateUser(CreateUserRequest request, CancellationToken cancellationToken);
}
public class AccountService(IAccountRepository accountRepository) : IBaseService, IAccountService  
{
    public async Task<StoredProcedureResponse> CreateUser(CreateUserRequest request, CancellationToken cancellationToken)
    {
       return await accountRepository.CreateUser(request, cancellationToken);
    }
}
