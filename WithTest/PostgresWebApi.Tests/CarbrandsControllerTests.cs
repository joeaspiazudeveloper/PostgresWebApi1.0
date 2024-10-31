using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using PostgresWebApi.Controllers;
using PostgresWebApi.Data;
using PostgresWebApi.Models;

namespace PostgresWebApi.Tests;

public class CarbrandsControllerTests : IClassFixture<ApiDbContextFixture>
{
    private readonly ApiDbContextFixture _apiDbContextFixture;
    private readonly ApiDbContext _dbContext;
    private readonly ILogger<CarbrandsController> _logger;

    public CarbrandsControllerTests(ApiDbContextFixture apiDbContextFixture)
    {
        _apiDbContextFixture = apiDbContextFixture;
        _dbContext = apiDbContextFixture._dbContext;
        _logger = Substitute.For<ILogger<CarbrandsController>>();
    }

    [Fact]
    public void Test1()
    {
        // Arrange
        var carbrand = new Carbrand
        {
            Description = "BYD",
            Name = "Test",
        };
        _dbContext.Carbrands.Add(carbrand);
        _dbContext.SaveChanges();

        var result = _dbContext.Carbrands.First();

        result.Should().NotBeNull();
    }

    [Fact]
    public async Task Get_WhenDataExist_ShouldReturnList()
    {
        // Arrange
        var controller = new CarbrandsController(_logger, _dbContext);

        // Act
        var result = await controller.Get();

        // Assert
        result.Should().NotBeNull();
        var okResult = result as OkObjectResult;
        var carBrands = okResult!.Value as List<Carbrand>;
        carBrands.Should().NotBeNull();
        carBrands.Should().HaveCountGreaterThanOrEqualTo(3);
    }

    [Fact]
    public async Task GetById_WhenDataExist_ShouldReturnList()
    {
        // Arrange
        var carBrandId = 1;
        var controller = new CarbrandsController(_logger, _dbContext);

        // Act
        var result = await controller.Get(carBrandId);

        // Assert
        result.Should().NotBeNull();
        var okResult = result as OkObjectResult;
        var carBrands = okResult!.Value as Carbrand;
        carBrands.Should().NotBeNull();
        carBrands!.Id.Should().Be(carBrandId);
    }
}
