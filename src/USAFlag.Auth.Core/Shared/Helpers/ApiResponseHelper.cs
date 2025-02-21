namespace USAFlag.Auth.Core.Shared.Helpers;

public static class ApiResponseHelper
{
    public static ApiResponse CreateSuccessApiResponse(object response, HttpStatusCode statusCode = HttpStatusCode.OK, string message = "Success")
    {
        return new ApiResponse
        {
            response = response,
            errorMessage = "",
            statusCode = (int)statusCode,
            status = true,
            message = message
        };
    }

    public static ApiResponse CreateFailedApiResponse(object response, HttpStatusCode statusCode, string errorMessage)
    {
        return new ApiResponse
        {
            response = response,
            errorMessage = errorMessage,
            statusCode = (int)statusCode,
            status = false,
            message = "Failed"
        };
    }
}