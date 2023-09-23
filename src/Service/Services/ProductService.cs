using AutoMapper;
using Data.IRepositories;
using Domain.Configuration;
using Domain.Entities.ProductFolder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Service.DTOs.Products;
using Service.Exceptions;
using Service.Extensions;
using Service.Interfaces;
using System.Linq.Expressions;

namespace Service.Services;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _repository;
    private readonly IMapper _mapper;

    public ProductService(IRepository<Product> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductResultDto> CreateAsync(ProductCreationDto dto)
    {
        if (await this.DoesProductExist(dto.Name))
            throw new AlreadyExistException($"Product with name '{dto.Name}' already exists.");

        var newProduct = _mapper.Map<Product>(dto);
        await _repository.AddAsync(newProduct);

        return _mapper.Map<ProductResultDto>(newProduct);
    }

    public async Task<IEnumerable<ProductResultDto>> RetrieveAllAsync(PaginationParams? paginationParams = null)
    {
        string[] inclusion = { "Category", "ProductAttachments", "ProductItems" };

        IQueryable<Product> query = _repository.GetAll(includes: inclusion);

        if (paginationParams is not null)
            query = query.ToPaginate(paginationParams);

        var products = await query.ToListAsync();

        return _mapper.Map<IEnumerable<ProductResultDto>>(products);
    }

    public async Task<ProductResultDto> RetrieveAsync(Expression<Func<Product, bool>> expression)
    {
        string[] inclusion = { "Category", "ProductAttachments", "ProductItems" };
        
        var theProduct = await _repository.GetAsync(expression, inclusion)
            ?? throw new NotFoundException("Product with such properties is not found.");

        return _mapper.Map<ProductResultDto>(theProduct);
    }

    public async Task<ProductResultDto> RetrieveAsync(long id)
    {
        string[] inclusion = { "Category", "ProductAttachments", "ProductItems" };

        var theProduct = await _repository.GetAsync(id, inclusion)
            ?? throw new NotFoundException("Product with such id is not found.");

        return _mapper.Map<ProductResultDto>(theProduct);
    }

    public async Task<ProductResultDto> ModifyAsync(ProductUpdateDto dto)
    {
        var theProduct = await _repository.GetAsync(dto.Id)
            ?? throw new NotFoundException("Product is not found.");

        if(!theProduct.Name.Equals(dto.Name, StringComparison.OrdinalIgnoreCase))
            if(await this.DoesProductExist(dto.Name))
                throw new AlreadyExistException($"Product with name '{dto.Name}' already exists.");

        _mapper.Map(dto, theProduct);
        _repository.Update(theProduct);
        await _repository.SaveAsync();

        return _mapper.Map<ProductResultDto>(theProduct);
    }

    public async Task<bool> RemoveAsync(long id, bool destroy = false)
    {
        var theProduct = await _repository.GetAsync(id)
            ?? throw new NotFoundException("Product with such id is not found.");

        if (destroy)
            _repository.Destroy(theProduct);
        else
            _repository.Delete(theProduct);
        
        return true;
    }

    private async ValueTask<bool> DoesProductExist(string name)
    {
        var theProduct = await _repository.GetAsync(p => p.Name.ToLower() == name.ToLower());
        return theProduct != null;
    }
}
