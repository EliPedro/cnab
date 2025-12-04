using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using WebSite.Database;
using WebSite.Entities;
using WebSite.Features.Store;
using WebSite.Features.Upload;

namespace UnitTest;

public class BalanceTests
{
    private UploadHandler handler;
    private StoreQuery storeQuery;
    private Mock<ILogger<UploadHandler>> logger;

    public BalanceTests()
    {
        logger = new Mock<ILogger<UploadHandler>>();
    }
    private static ApplicationDbContext InMemoryDb()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }

    [Fact(DisplayName = "Calculate store balance correctly")]
    public async Task CalculateStoreBalance()
    {
        // Arrange
        using var context = InMemoryDb();
        handler = new UploadHandler(context, new UploadValidator(), logger.Object);

        var line = "3201903010000014200096206760174753****3153153453JOÃO MACEDO   BAR DO JOÃO  ";
        var parser = new ParseUploadedFile(line);
        var command = parser.Parse();

        await handler.HandleAsync(command.Value);

        storeQuery = new StoreQuery(context);

        // Act
        var storeResponses = await storeQuery.HandleAsync(default);

        var storeResponse = storeResponses.FirstOrDefault();
        // Assert
        Assert.NotNull(storeResponse);
        Assert.Equal("BAR DO JOÃO", storeResponse.Name);
        Assert.Equal("JOÃO MACEDO", storeResponse.Owner);
        var balance = storeResponse.Transactions.Sum(t => t.Amount);
        Assert.Equal(-142, balance);
    }
}