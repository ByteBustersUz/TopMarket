using AutoMapper;
using Data.IRepositories;
using Domain.Entities.AttachmentFolder;
using Domain.Entities.ProductFolder;
using Microsoft.Extensions.Logging;
using MockQueryable.Moq;
using Moq;
using Service.DTOs.Products;
using Service.Interfaces;
using Service.Mappers;
using System.Linq.Expressions;

namespace Service.Services;

public class ProductServiceTests
{
    private readonly Mock<ILogger<ProductService>> loggerMock;
    private readonly Mock<IRepository<Product>> productRepositoryMock;
    private readonly Mock<IRepository<Attachment>> attachmentRepositoryMock;
    private readonly Mock<IRepository<ProductAttachment>> productAttachmentRepositoryMock;
    private readonly Mock<IRepository<Category>> categoryRepositoryMock;
    private readonly IMapper mapper;
    private readonly IProductService productService;
    private readonly IAttachmentService attachmentService;
    private readonly IProductAttachmentService productAttachmentService;

    public ProductServiceTests()
    {
        this.loggerMock = new Mock<ILogger<ProductService>>();
        this.productRepositoryMock = new Mock<IRepository<Product>>();
        this.attachmentRepositoryMock = new Mock<IRepository<Attachment>>();
        this.categoryRepositoryMock = new Mock<IRepository<Category>>();
        this.productAttachmentRepositoryMock = new Mock<IRepository<ProductAttachment>>();
        this.mapper = new MapperConfiguration(config => config.AddProfile<MappingProfile>()).CreateMapper();
        this.attachmentService = new AttachmentService(mapper, attachmentRepositoryMock.Object);
        this.productAttachmentService = new ProductAttachmentService(mapper, productRepositoryMock.Object, productAttachmentRepositoryMock.Object, attachmentRepositoryMock.Object);
        this.productService = new ProductService(loggerMock.Object, mapper, productRepositoryMock.Object, attachmentService, categoryRepositoryMock.Object, productAttachmentService);
    }

    [Fact]
    public async Task AddAsync_ShouldReturnCreatedProduct()
    {
        // Arrange
        var category = new Category
        {
            Id = 1L,
            Name = "IPhones",
            Description = "Excepteur sint occaecat cupidatat non proident."
        };

        var product = new ProductCreationDto
        {
            Name = "IPhone SE 2020",
            Description = "Lorem ipsum dolor sit amet. Excepteur sint occaecat cupidatat non proident, mollit anim id est laborum.",
            CategoryId = category.Id,
        };

        this.categoryRepositoryMock.Setup(cr => cr.GetAsync(category.Id, It.IsAny<string[]>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(category);

        this.productRepositoryMock.Setup(pr => pr.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
            .Callback<Product, CancellationToken>((entity, cancellationToken) => { 
                entity.Id = 1;
                entity.Category = category; })
            .Returns(Task.CompletedTask);

        // Act
        var result = await this.productService.CreateAsync(product);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Category);
        Assert.Equal(1, result.Id);
        Assert.Equal(product.Name, result.Name);
        Assert.Equal(product.Description, result.Description);

        // Verify
        this.categoryRepositoryMock.Verify(cr => cr.GetAsync(It.IsAny<long>(), It.IsAny<string[]>(), It.IsAny<CancellationToken>()), Times.Once);
        this.productRepositoryMock.Verify(pr => pr.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ModifyAsync_ShouldReturnModifiedProduct()
    {
        var category = new Category
        {
            Id = 1L,
            Name = "IPhones",
            Description = "Excepteur sint occaecat cupidatat non proident."
        };

        var update = new ProductUpdateDto
        {
            Id = 1L,
            CategoryId = category.Id,
            Name = "IPhone SE 2020 64GB Space Gray",
            Description = "This is an IPhone SE 2020 64GB in color of Space Gray..."
        };

        var existing = new Product
        {
            Id = 1L,
            Name = "IPhone SE 2020",
            Description = "Lorem ipsum dolor sit amet. Excepteur sint occaecat cupidatat non proident, mollit anim id est laborum.",
            CategoryId = category.Id,
        };

        this.categoryRepositoryMock.Setup(cr => cr.GetAsync(category.Id, It.IsAny<string[]>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(category);

        this.productRepositoryMock.Setup(pr => pr.GetAsync(update.Id, It.IsAny<string[]>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(existing);

        var result = await this.productService.ModifyAsync(update);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnValidProduct()
    {
        // Arrange
        long id = 1L;

        var existing = new Product
        {
            Id = id,
            Name = "IPhone SE 2020",
            Description = "Lorem ipsum dolor sit amet. Excepteur sint occaecat cupidatat non proident, mollit anim id est laborum.",
            CategoryId = 1L,
        };

        var category = new Category
        {
            Id = 1L,
            Name = "IPhones",
            Description = "Excepteur sint occaecat cupidatat non proident."
        };

        this.productRepositoryMock.Setup(pr => pr.GetAsync(id, It.IsAny<string[]>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(existing);

        // Act
        var result = await this.productService.RetrieveAsync(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existing.Id, result.Id);
        Assert.Equal(existing.Name, result.Name);
        Assert.Equal(existing.Description, result.Description);

        // Verify
        this.productRepositoryMock.Verify(pr => pr.GetAsync(id, It.IsAny<string[]>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByCategoryIdAsync_ShouldReturnValidProducts()
    {
        // Arrange
        long categoryId = 1L;
        var products = this.getFakeProducts();

        this.productRepositoryMock.Setup(pr => pr.GetAll(p => p.CategoryId == categoryId, It.IsAny<bool>(), It.IsAny<string[]>()))
            .Returns(products.Where(p => p.CategoryId == categoryId).BuildMock());
        
        // Act
        var result = await this.productService.RetrieveByCategoryIdAsync(categoryId);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(2, result.Count());

        // Verify
        this.productRepositoryMock.Verify(pr => pr.GetAll(p => p.CategoryId == categoryId, It.IsAny<bool>(), It.IsAny<string[]>()), Times.Once);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnValidProducts()
    {
        // Arrange
        var products = this.getFakeProducts();

        this.productRepositoryMock.Setup(pr => pr.GetAll(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<bool>(), It.IsAny<string[]>()))
            .Returns(products.BuildMock());

        // Act
        var result = await this.productService.RetrieveAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(4, result.Count());

        // Verify
        this.productRepositoryMock.Verify(pr => pr.GetAll(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<bool>(), It.IsAny<string[]>()), Times.Once);
    }

    [Fact]
    public async Task RemoveAsync_ShouldReturnTrue()
    {
        // Arrange
        long id = 1;
        var product = this.getFakeProducts().First();

        this.productRepositoryMock.Setup(pr => pr.GetAsync(id, It.IsAny<string[]>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        this.productRepositoryMock.Setup(pr => pr.Delete(It.IsAny<Product>()))
            .Callback<Product>(p => p.IsDeleted = true);

        // Act
        var result = await this.productService.RemoveAsync(id);
        
        // Assert
        Assert.True(result);
        Assert.True(product.IsDeleted);

        // Verify
        this.productRepositoryMock.Verify(pr => pr.GetAsync(It.IsAny<long>(), It.IsAny<string[]>(), It.IsAny<CancellationToken>()), Times.Once);
        this.productRepositoryMock.Verify(pr => pr.Delete(It.IsAny<Product>()), Times.Once);
        this.productRepositoryMock.Verify(pr => pr.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task RemoveAsync_Destroy_ShouldReturnTrue()
    {
        // Arrange
        long id = 1;
        
        var products = this.getFakeProducts();
        
        var productToDestroy = products.Single(p => p.Id == id);

        this.productRepositoryMock.Setup(pr => pr.GetAsync(id, It.IsAny<string[]>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(productToDestroy);

        this.productRepositoryMock.Setup(pr => pr.Destroy(It.IsAny<Product>()))
            .Callback<Product>(p => products.Remove(p));

        // Act
        var result = await this.productService.RemoveAsync(id, destroy: true);

        // Assert
        Assert.True(result);
        Assert.DoesNotContain(productToDestroy, products);

        // Verify
        this.productRepositoryMock.Verify(pr => pr.GetAsync(It.IsAny<long>(), It.IsAny<string[]>(), It.IsAny<CancellationToken>()), Times.Once);
        this.productRepositoryMock.Verify(pr => pr.Destroy(It.IsAny<Product>()), Times.Once);
        this.productRepositoryMock.Verify(pr => pr.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    private IList<Product> getFakeProducts()
    {
        return [
            new()
            {
                Id = 1,
                Name = "IPhone SE 2020",
                Description = "Lorem ipsum dolor sit amet. Excepteur sint occaecat cupidatat non proident, mollit anim id est laborum.",
                CategoryId = 1L,
            },
            new()
            {
                Id = 2,
                Name = "IPhone 15 Pro",
                Description = "Excepteur sint occaecat cupidatat non proident, mollit anim id est laborum. Lorem ipsum dolor sit amet.",
                CategoryId = 1L,
            },
            new()
            {
                Id = 3,
                Name = "Samsung Galaxy S24 Ultra",
                Description = "Amet excepteur sint occaecat cupidatat non proident, mollit anim id est laborum.",
                CategoryId = 2L,
            },
            new()
            {
                Id = 4,
                Name = "Redmi K20 Pro",
                Description = "Lint occaecat cupidatat non proident, mollit anim id est laborum.",
                CategoryId = 3L,
            }
        ];
    }
}
