using Application.Dtos.Table.Requests;
using Application.Repositories;
using Application.Services;
using Domain.Enums;
using FluentAssertions;
using Moq;

namespace Restaurant.Tests.Integration.CQRS;

[TestFixture]
public class TableWithCqrsEndpointTest
{
    private Mock<ITableRepository> _mockRepository;
    private ITableService _tableService;

    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<ITableRepository>();
        _tableService = new TableService(_mockRepository.Object);
    }

    [Test]
    public async Task AddTableWithCqrsEndpoint()
    {
        //Arrange
        Mock<ITableRepository> _mockRepository = new Mock<ITableRepository>();
        TableService _tableService = new TableService(_mockRepository.Object);

        var request = new CreateTableRequestModel(1223, 32, TableType.Cabin);

        //Act
        var result = await this._tableService.CreateAsync(request);

        //Assert
        var resultTable = await this._mockRepository.Object.GetAsync(result.Id);
        resultTable.Should().NotBeNull();
    }
}