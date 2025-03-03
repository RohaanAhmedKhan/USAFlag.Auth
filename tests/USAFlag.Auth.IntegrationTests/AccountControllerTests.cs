namespace USAFlag.Auth.IntegrationTests;

public class AccountControllerTests : IClassFixture<WebApplicationFactory<Program>>, IDisposable
{
    private readonly WebApplicationFactory<Program> factory;
    private readonly NpgsqlConnection dbConnection;

    public AccountControllerTests(WebApplicationFactory<Program> factory)
    {
        this.factory = factory;

        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Test.json")
            .Build();

        this.dbConnection = new NpgsqlConnection(config.GetConnectionString("USAFlagConnectionString"));
    }

    [Fact]
    public async Task CreateUser_ValidRequest_ReturnsOkAndPersistsUser()
    {
        // Arrange
        var client = factory.CreateClient();
        var request = new
        {
            userName = "TestUser",
            emailAddress = "test@example.com",
            password = "ValidPassword123!"
        };

        // Act
        var response = await client.PostAsJsonAsync("/api/v1/account/createuser", request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // Verify database persistence
        var user = await dbConnection.QueryFirstOrDefaultAsync(
            "SELECT * FROM auth.tbl_users WHERE email_address = @EmailAddress",
            new { EmailAddress = request.emailAddress });

        Assert.NotNull(user);
        Assert.Equal(request.userName, user.user_name);
    }

    [Fact]
    public async Task CreateUser_DuplicateEmail_ReturnsConflict()
    {
        // Arrange
        var client = factory.CreateClient();
        var request = new
        {
            userName = "TestUser",
            emailAddress = "Rohaan@gmail.com",
            password = "ValidPassword123!"
        };

        // First request
        await client.PostAsJsonAsync("/api/v1/account/createuser", request);

        // Second request
        var response = await client.PostAsJsonAsync("/api/v1/account/createuser", request);

        // Assert
        // Assert
        response.EnsureSuccessStatusCode(); // Verify 200-299 status code

        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();

        // Verify API response structure
        Assert.NotNull(apiResponse);
        Assert.Equal(409, apiResponse.statusCode);
        Assert.NotNull(apiResponse.response);
    }

    public void Dispose()
    {
        // Cleanup code
        dbConnection.Execute("DELETE FROM auth.tbl_users WHERE email_address LIKE '%@example.com'");
        dbConnection.Close();
        dbConnection.Dispose();

        // Add this line to suppress finalization
        GC.SuppressFinalize(this);
    }
}
