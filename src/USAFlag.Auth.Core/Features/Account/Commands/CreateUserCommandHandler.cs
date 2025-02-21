namespace USAFlag.Auth.Core.Features.Account.Commands;

public class CreateUserCommandHandler(IAccountService accountService) : IRequestHandler<GenericCommand<CreateUserRequest>, ApiResponse> 
{

    public async Task<ApiResponse> Handle(GenericCommand<CreateUserRequest> command, CancellationToken cancellationToken)
    {
        var apiResponse = new ApiResponse();
        try
        {
            var response = await accountService.CreateUser(command.request, cancellationToken);

            // Handle the response
            switch (response.ReturnValue)
            {
                case 0:
                    apiResponse = ApiResponseHelper.CreateSuccessApiResponse(response, HttpStatusCode.OK, "User created successfully.");
                    break;
                case 1:
                    apiResponse = ApiResponseHelper.CreateFailedApiResponse(response, HttpStatusCode.Conflict, "User already exists.");
                    break;
                default:
                    apiResponse = ApiResponseHelper.CreateFailedApiResponse(response, HttpStatusCode.Conflict, "Error occurred while creating user.");
                    break;
            }
        }
        catch (Exception ex)
        {
            //_logger.LogError("Error occurred while updating profile: {Message}", ex.Message);
            apiResponse = ApiResponseHelper.CreateFailedApiResponse(null, HttpStatusCode.InternalServerError, "Error occurred while creating user.");
        }

        return apiResponse;
    }
}
 