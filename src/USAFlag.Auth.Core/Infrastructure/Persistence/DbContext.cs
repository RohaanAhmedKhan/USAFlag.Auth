namespace USAFlag.Auth.Infrastructure.Persistence;

public class DbContext
{
    private readonly string connectionString;

    public DbContext(IConfiguration configuration)
    {
        this.connectionString = configuration.GetConnectionString("USAFlagConnectionString");
    }

    public async Task<NpgsqlConnection> CreateOpenConnectionAsync()
    {
        var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();
        return connection;
    }

    public async Task<TResponse> ExecuteStoredProcedureAsync<TResponse>(string procedureName, DynamicParameters parameters, CancellationToken cancellationToken)
    {
        using var connection = await CreateOpenConnectionAsync();
        using var transaction = await connection.BeginTransactionAsync();
        try
        {
            parameters.Add("result", 0, DbType.Int32, ParameterDirection.InputOutput);

            await connection.ExecuteAsync(
                procedureName,
                parameters,
                commandType: CommandType.StoredProcedure
            );
            var result = parameters.Get<TResponse>("result");
            await transaction.CommitAsync();
            return result;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            //_logger.LogError(ex, "Error executing stored procedure {ProcedureName}", procedureName);
            throw; // Re-throw the exception
        }
    }

}
