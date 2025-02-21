namespace USAFlag.Auth.Core.Shared.Commands;

public class GenericCommand<TRequest> : IRequest<ApiResponse> where TRequest : class
{
    public TRequest request { get; set; }
    public int userId { get; set; }

    public GenericCommand(TRequest request, int userId)
    {
        this.request = request;
        this.userId = userId;
    }
}
