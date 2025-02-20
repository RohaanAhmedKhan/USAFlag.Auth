namespace USAFlag.Auth.Core.Application.Handlers.Commands;

public class GenericCommand<TRequest> : IRequest<ApiResponse>
{
    public TRequest Request { get; set; }
    public int UserId { get; set; }

    public GenericCommand(TRequest request, int userId)
    {
        Request = request;
        UserId = userId;
    }
}
