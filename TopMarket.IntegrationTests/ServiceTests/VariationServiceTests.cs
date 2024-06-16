using AutoMapper;
using Data.IRepositories;
using Domain.Entities.ProductFolder;
using MockQueryable.Moq;
using Moq;
using Service.DTOs.Variations;
using Service.Interfaces;
using Service.Mappers;
using Service.Services;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TopMarket.IntegrationTests.ServiceTests;

public class VariationServiceTests
{
    private readonly IMapper mapper;
    private readonly Mock<IRepository<Variation>> variationRepoMock;
    private readonly Mock<IRepository<Category>> categoryRepoMock;
    private readonly IVariationService variationService;
    private readonly Mock<IProductConfigurationService> productConfigurationService;

    public VariationServiceTests()
    {
        this.mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
        this.variationRepoMock = new Mock<IRepository<Variation>>();
        this.categoryRepoMock = new Mock<IRepository<Category>>();
        this.productConfigurationService = new Mock<IProductConfigurationService>();
        this.variationService = new VariationService(mapper, variationRepoMock.Object, categoryRepoMock.Object, productConfigurationService.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnCreatedVariation()
    {
        // Arrange
        var category = new Category
        {
            Id = 1,
            Name = "FirstCategory",
            Description = "FirstCategoryDescription..."
        };

        var variation = new VariationCreationDto
        { 
            Name = "FirstVariation",
            CategoryId = 1
        };

        this.categoryRepoMock.Setup(cr => cr.GetAsync(category.Id, It.IsAny<string[]>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(category);

        this.variationRepoMock.Setup(vr => vr.AddAsync(It.IsAny<Variation>(), It.IsAny<CancellationToken>()))
            .Callback<Variation, CancellationToken>((entity, cancellationToken) => {
                entity.Id = 1;
                entity.Category = category; })
            .Returns(Task.CompletedTask);

        // Act
        var result = await this.variationService.CreateAsync(variation);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal(variation.Name, result.Name);
        Assert.Equal(variation.CategoryId, result.Category.Id);

        // Verify
        this.categoryRepoMock.Verify(cr => cr.GetAsync(It.IsAny<long>(), It.IsAny<string[]>(), It.IsAny<CancellationToken>()), Times.Once);
        this.variationRepoMock.Verify(cr => cr.AddAsync(It.IsAny<Variation>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ModifyAsync_ShouldReturnModifiedVariation()
    {
        // Arrange
        var category = new Category
        {
            Id = 1L,
            Name = "IPhones",
            Description = "Excepteur sint occaecat cupidatat non proident."
        };

        var existing = new Variation
        {
            Id = 1,
            Name = "OldVariation",
            CategoryId = category.Id,
            Category = category
        };

        var update = new VariationUpdateDto
        {
            Id = 1,
            Name = "NewVariation",
            CategoryId = category.Id
        };

        this.variationRepoMock.Setup(vr => vr.GetAsync(update.Id, It.IsAny<string[]>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(existing);

        // Act
        var result = await this.variationService.UpdateAsync(update);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existing.Name, update.Name);
        Assert.Equal(update.Name, result.Name);
        Assert.Equal(category.Id, result.Category.Id);
        Assert.Equal(category.Name, result.Category.Name);

        // Verify
        this.variationRepoMock.Verify(vr => vr.GetAsync(It.IsAny<long>(), It.IsAny<string[]>(), It.IsAny<CancellationToken>()), Times.Once);
        this.variationRepoMock.Verify(vr => vr.Update(It.IsAny<Variation>()), Times.Once);
    }

    [Fact]
    public async Task RemoveAsync_ShouldReturnTrue()
    {
        // Arrange
        long id = 1;
        var variationToDelete = new Variation
        {
            Id = id,
            Name = "Variation1"
        };

        this.variationRepoMock.Setup(vr => vr.GetAsync(id, It.IsAny<string[]>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(variationToDelete);

        this.variationRepoMock.Setup(vr => vr.Delete(It.IsAny<Variation>()))
            .Callback<Variation>(v => v.IsDeleted = true);

        // Act
        var result = await this.variationService.DeleteAsync(id);

        // Assert
        Assert.True(result);
        Assert.True(variationToDelete.IsDeleted);

        // Verify
        this.variationRepoMock.Verify(vr => vr.GetAsync(It.IsAny<long>(), It.IsAny<string[]>(), It.IsAny<CancellationToken>()), Times.Once);
        this.variationRepoMock.Verify(vr => vr.Delete(It.IsAny<Variation>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnValidVariation()
    {
        // Arrange
        long id = 1;
        var variation = new Variation
        {
            Id= id,
            Name = "Variation1",
            CategoryId = 1,
        };

        this.variationRepoMock.Setup(vr => vr.GetAsync(id, It.IsAny<string[]>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(variation);

        // Act
        var result = await this.variationService.GetByIdAsync(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Variation1", result.Name);
        Assert.Equal([], result.VariationOptions);

        // Verify
        this.variationRepoMock.Verify(vr => vr.GetAsync(It.IsAny<long>(), It.IsAny<string[]>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnList()
    {
        // Arrange
        var variations = this.getFakeVariations();

        this.variationRepoMock.Setup(vr => vr.GetAll(It.IsAny<Expression<Func<Variation, bool>>>(), It.IsAny<bool>(), It.IsAny<string[]>()))
            .Returns(variations.BuildMock());

        // Act
        var result = await this.variationService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(variations.Count, result.Count());

        // Verify
        this.variationRepoMock.Verify(vr => vr.GetAll(It.IsAny<Expression<Func<Variation, bool>>>(), It.IsAny<bool>(), It.IsAny<string[]>()), Times.Once);
    }

    private IList<Variation> getFakeVariations()
    {
        return [
            new()
            {
                Id = 1,
                Name = "Variation1",
                CategoryId = 1
            },
            new()
            {
                Id = 2,
                Name = "Variation2",
                CategoryId = 2
            }
        ];
    }
}
