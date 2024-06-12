using AutoMapper;
using Data.IRepositories;
using Domain.Entities.Addresses;
using Microsoft.Extensions.Logging;
using MockQueryable.Moq;
using Moq;
using Service.DTOs.Addresses;
using Service.Interfaces;
using Service.Mappers;
using Service.Services;
using System.Linq.Expressions;

namespace TopMarket.IntegrationTests.ServiceTests;

public class AddressServiceTests
{
    private readonly Mock<ILogger<AddressService>> loggerMock;
    private readonly Mock<IRepository<Address>> repositoryMock;
    private readonly IMapper mapper;
    private readonly IAddressService service;

    public AddressServiceTests()
    {
        this.loggerMock = new Mock<ILogger<AddressService>>();
        this.repositoryMock = new Mock<IRepository<Address>>();
        this.mapper = new MapperConfiguration(config => config.AddProfile<MappingProfile>()).CreateMapper();
        this.service = new AddressService(logger: loggerMock.Object, mapper: mapper, repository: repositoryMock.Object);
    }

    [Fact]
    public async Task AddAsync_ShouldReturnCreatedAddress()
    {
        // Arrange
        var address = new AddressCreationDto
        {
            Street = "Amir Temur",
            CountryId = 1,
            DistrictId = 1,
            DoorCode = "33",
            Floor = "Ground floor",
            Home = "33",
            RegionId = 1
        };

        this.repositoryMock.Setup(r => r.AddAsync(It.IsAny<Address>(), It.IsAny<CancellationToken>()))
            .Callback<Address, CancellationToken>((entity, cancellationToken) => entity.Id = 1)
                .Returns(Task.CompletedTask);

        // Act
        var result = await this.service.CreateAsync(address);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal(address.Street, result.Street);
        Assert.Equal(address.DoorCode, result.DoorCode);
        Assert.Equal(address.Floor, result.Floor);
        Assert.Equal(address.Home, result.Home);

        // Verify
        this.repositoryMock.Verify(r => r.AddAsync(It.IsAny<Address>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ModifyAsync_ShouldReturnResultDto()
    {
        // Arrange
        var update = new AddressUpdateDto
        {
            Id = 1,
            CountryId = 1,
            DistrictId = 1,
            RegionId = 1,
            DoorCode = "44",
            Floor = "1st floor",
            Home = "44",
            Street = "Nurafshon"
        };

        var existing = new Address
        {
            Id = 1,
            Street = "Amir Temur",
            CountryId = 1,  
            DistrictId = 1,
            DoorCode = "33",
            Floor = "Ground floor",
            Home = "33",
            RegionId = 1
        };

        this.repositoryMock.Setup(r => r.GetAsync(update.Id, It.IsAny<string[]>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(existing);

        // Act
        var result = await this.service.ModifyAsync(update);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(update.Id, result.Id);
        Assert.Equal(update.Street, result.Street);
        Assert.Equal(update.Home, result.Home);
        Assert.Equal(update.DoorCode, result.DoorCode);
        Assert.Equal(update.Floor, result.Floor);

        // Verify
        this.repositoryMock.Verify(r => r.GetAsync(update.Id, It.IsAny<string[]>(), It.IsAny<CancellationToken>()), Times.Once);
        this.repositoryMock.Verify(r => r.Update(It.IsAny<Address>()), Times.Once);
        this.repositoryMock.Verify(r => r.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task RemoveAsync_ShouldReturnTrue()
    {
        // Arrange
        long id = 1;
        var existing = new Address
        {
            Id = id,
            Street = "Amir Temur",
            CountryId = 1,
            DistrictId = 1,
            DoorCode = "33",
            Floor = "Ground floor",
            Home = "33",
            RegionId = 1
        };

        this.repositoryMock.Setup(r => r.GetAsync(id, It.IsAny<string[]>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(existing);

        // Act
        var result = await this.service.RemoveAsync(id);

        // Assert
        Assert.True(result);

        // Verify
        this.repositoryMock.Verify(r => r.GetAsync(id, It.IsAny<string[]>(), It.IsAny<CancellationToken>()), Times.Once);
        this.repositoryMock.Verify(r => r.Delete(It.IsAny<Address>()), Times.Once);
    }

    [Fact]
    public async Task RetrieveById_IfExists()
    {
        // Arrange
        long id = 1;
        var existing = new Address
        {
            Id = id,
            Street = "Amir Temur",
            CountryId = 1,
            DistrictId = 1,
            DoorCode = "33",
            Floor = "Ground floor",
            Home = "33",
            RegionId = 1
        };

        this.repositoryMock.Setup(r => r.GetAsync(id, It.IsAny<string[]>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(existing);

        // Act
        var result = await this.service.RetrieveByIdAsync(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal(existing.Street, result.Street);

        // Verify
        this.repositoryMock.Verify(r => r.GetAsync(id, It.IsAny<string[]>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task RetrieveAll_ShouldReturnList()
    {
        // Arrange
        var addresses = this.getFakeAddresses();

        this.repositoryMock.Setup(r => r.GetAll(It.IsAny<Expression<Func<Address, bool>>>(), It.IsAny<bool>(), It.IsAny<string[]>()))
            .Returns(addresses.BuildMock());

        // Act
        var result = await this.service.RetrieveAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(addresses.Count, result.Count());

        // Verify
        this.repositoryMock.Verify(r => r.GetAll(It.IsAny<Expression<Func<Address, bool>>>(), It.IsAny<bool>(), It.IsAny<string[]>()), Times.Once);
    }

    private IList<Address> getFakeAddresses()
    {
        var addresses = new List<Address>
        {
            new() {
                Id = 1,
                Street = "Amir Temur",
                CountryId = 1,
                DistrictId = 1,
                DoorCode = "33",
                Floor = "Ground floor",
                Home = "33",
                RegionId = 1 },

            new() {
                Id = 2,
                CountryId = 1,
                DistrictId = 2,
                DoorCode = "3",
                Floor = "4",
                Home = "5",
                RegionId = 6,
                Street = "Ming o'rik" }
        };

        return addresses;
    }
}
