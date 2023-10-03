using AutoMapper;
using Data.IRepositories;
using Domain.Entities.ProductFolder;
using Domain.Entities.Shopping;
using Domain.Entities.UserFolder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Service.DTOs.Carts;
using Service.DTOs.ProductItems;
using Service.Exceptions;
using Service.Interfaces;
using System.Net.Http;

namespace Service.Services;

public class CartService : ICartService
{
    private readonly IMapper _mapper;
    private readonly IProductItemService _productItemService;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<ShoppingCart> _cartRepository;
    private readonly IRepository<ShoppingCartItem> _cartItemRepository;

    public CartService(IMapper mapper,
                       IRepository<ShoppingCart> cartRepository,
                       IRepository<ShoppingCartItem> cartItemRepository,
                       IProductItemService productItemService,
                       IRepository<User> userRepository)
    {
        _mapper = mapper;
        _productItemService = productItemService;
        _cartRepository = cartRepository;
        _cartItemRepository = cartItemRepository;
        _userRepository = userRepository;
    }

    public async Task<CartResultDto> CreateAsync()
    {
        var newCart = new ShoppingCart { };

        await _cartRepository.AddAsync(newCart);    
        await _cartRepository.SaveAsync();

        return _mapper.Map<CartResultDto>(newCart);
    }

    public async Task AddItemToCartAsync(long cartId, long productItemId)
    {
        var items = await _cartItemRepository.GetAll(i => i.CartId.Equals(cartId), isNoTracked: false).ToListAsync()
            ?? throw new NotFoundException($"Cart with id = '{cartId}' is not found.");
        
        var theProductItem = await _productItemService.GetByIdAsync(productItemId)
            ?? throw new NotFoundException($"ProductItem with id = '{productItemId}' is not found.");

        var theCartItem = items.FirstOrDefault(i => i.ProductItemId.Equals(productItemId));
        if (theCartItem is null)
            items.Add(new ShoppingCartItem
            {
                CartId = cartId,
                ProductItemId = productItemId,
                Quantity = 1,
                Price = theProductItem.Price,
            });
        else
            theCartItem.Quantity += 1;
    
        await _cartItemRepository.SaveAsync();
    }

    public async Task<bool> ClearCartAsync(long cartId)
    {
        var items = await _cartItemRepository.GetAll(i => i.CartId.Equals(cartId), isNoTracked: false).ToListAsync()
            ?? throw new NotFoundException($"Cart with id = '{cartId}' is not found.");
        
        foreach (var item in items)
            item.IsDeleted = true;
        
        await _cartItemRepository.SaveAsync();

        return true;
    }

    public async Task<ICollection<CartItemResultDto>> RetrieveAllItemsAsync(long cartId)
    {
        var items = await _cartItemRepository.GetAll(i => i.CartId.Equals(cartId), isNoTracked: false).ToListAsync();
        return _mapper.Map<ICollection<CartItemResultDto>>(items);
    }
}