using Microsoft.EntityFrameworkCore;
using PostgresWebApi.Data;

namespace PostgresWebApi.Tests;

public class ApiDbContextFixture : IDisposable
{
    public ApiDbContext _dbContext { get; private set; }

    public ApiDbContextFixture()
    {
        var options = new DbContextOptionsBuilder<ApiDbContext>()
            .UseSqlite("DataSource=:memory:", x => x.MigrationsAssembly(typeof(ApiDbContext).Assembly.FullName))
            .Options;
        _dbContext = new ApiDbContext(options);
        _dbContext.Database.OpenConnection();
        _dbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
        GC.Collect();
        GC.SuppressFinalize(this);
    }
}