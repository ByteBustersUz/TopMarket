using Domain.Entities.Addresses;
using Domain.Entities.AttachmentFolder;
using Domain.Entities.OrderFolder;
using Domain.Entities.Payment;
using Domain.Entities.ProductFolder;
using Domain.Entities.Shopping;
using Domain.Entities.UserFolder;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    //Addresses
    public DbSet<Region> Regions { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<District> Districts { get; set; }

    //Attachments
    public DbSet<Attachment> Attachments { get; set; }

    //Orders
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }
    public DbSet<ShippingMethod> ShippingMethods { get; set; }

    //Payments
    public DbSet<PaymentType> PaymentTypes { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }

    //Products
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Variation> Variations { get; set; }
    public DbSet<Promotion> Promotions { get; set; }
    public DbSet<ProductItem> ProductItems { get; set; }
    public DbSet<VariationOption> VarationOptions { get; set; }
    public DbSet<PromotionCategory> PromotionCategories { get; set; }
    public DbSet<ProductConfiguration> ProductConfigurations {get; set;}
    public DbSet<ProductAttachment> ProductAttachments {get; set; }
    public DbSet<ProductItemAttachment> ProductItemAttachments {get; set; }

    //Shoppings
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

    //Users
    public DbSet<User> Users { get; set; }
    public DbSet<UserReview> UserReviews { get; set; }
    public DbSet<UserAddress> UserAddresses { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region FluntApi

        //Address
        modelBuilder.Entity<Address>()
            .HasOne(address => address.Country)
            .WithMany()
            .HasForeignKey(address => address.CountryId);

        modelBuilder.Entity<Address>()
            .HasOne(address => address.Region)
            .WithMany()
            .HasForeignKey(address => address.RegionId);
        
        modelBuilder.Entity<Address>()
            .HasOne(address => address.District)
            .WithMany()
            .HasForeignKey(address => address.DistrictId);


        //District
        modelBuilder.Entity<District>()
            .HasOne(district => district.Region)
            .WithMany()
            .HasForeignKey(district => district.RegionId);


        //Region
        modelBuilder.Entity<Region>()
            .HasOne(region => region.Country)
            .WithMany()
            .HasForeignKey(region => region.CountryId);


        //Order
        modelBuilder.Entity<Order>()
            .HasOne(order => order.User)
            .WithMany()
            .HasForeignKey(order => order.UserId);

        modelBuilder.Entity<Order>()
            .HasOne(order => order.PaymentMethod)
            .WithMany()
            .HasForeignKey(order => order.PaymentMethodId);

        modelBuilder.Entity<Order>()
            .HasOne(order => order.Address)
            .WithMany()
            .HasForeignKey(order => order.AddressId);

        modelBuilder.Entity<Order>()
            .HasOne(order => order.ShippingMethod)
            .WithMany()
            .HasForeignKey(order => order.ShippingMethodId);

        modelBuilder.Entity<Order>()
            .HasOne(order => order.Status)
            .WithMany()
            .HasForeignKey(order => order.StatusId);


        //OrderLine
        modelBuilder.Entity<OrderLine>()
            .HasOne(orderLine => orderLine.ProductItem)
            .WithMany()
            .HasForeignKey(orderLine => orderLine.ProductItemId);

        modelBuilder.Entity<OrderLine>()
            .HasOne(orderLine => orderLine.Order)
            .WithMany()
            .HasForeignKey(orderLine => orderLine.OrderId);


        //PaymentMethod
        modelBuilder.Entity<PaymentMethod>()
            .HasOne(paymentMethod => paymentMethod.User)
            .WithMany()
            .HasForeignKey(paymentMethod => paymentMethod.UserId);

        modelBuilder.Entity<PaymentMethod>()
            .HasOne(paymentMethod => paymentMethod.PaymentType)
            .WithMany()
            .HasForeignKey(paymentMethod => paymentMethod.PaymentTypeId);


        //Product
        modelBuilder.Entity<Product>()
            .HasOne(product => product.Category)
            .WithMany()
            .HasForeignKey(product => product.CategoryId);


        //ProductAttachment
        modelBuilder.Entity<ProductAttachment>()
            .HasOne(productAttachment => productAttachment.Product)
            .WithMany()
            .HasForeignKey(productAttachment => productAttachment.ProductId);

        modelBuilder.Entity<ProductAttachment>()
            .HasOne(productAttachment => productAttachment.Attachment)
            .WithMany()
            .HasForeignKey(productAttachment => productAttachment.AttachmentId);


        //ProductItemAttachment
        modelBuilder.Entity<ProductItemAttachment>()
            .HasOne(productItemAttachment => productItemAttachment.ProductItem)
            .WithMany()
            .HasForeignKey(productItemAttachment => productItemAttachment.ProductItemId);

        modelBuilder.Entity<ProductAttachment>()
            .HasOne(productAttachment => productAttachment.Attachment)
            .WithMany()
            .HasForeignKey(productAttachment => productAttachment.AttachmentId);


        //ProductConfiguration
        modelBuilder.Entity<ProductConfiguration>()
            .HasOne(productConfiguration => productConfiguration.ProductItem)
            .WithMany()
            .HasForeignKey(productConfiguration => productConfiguration.ProductItemId);
        
        modelBuilder.Entity<ProductConfiguration>()
            .HasOne(productConfiguration => productConfiguration.VariationOption)
            .WithMany()
            .HasForeignKey(productConfiguration => productConfiguration.VariationOptionId);


        //ProductItem
        modelBuilder.Entity<ProductItem>()
            .HasOne(productItem => productItem.Product)
            .WithMany()
            .HasForeignKey(productItem => productItem.ProductId);


        //PromotionCategory
        modelBuilder.Entity<PromotionCategory>()
            .HasOne(promotionCategory => promotionCategory.Promotion)
            .WithMany()
            .HasForeignKey(promotionCategory => promotionCategory.PromotionId);

        modelBuilder.Entity<PromotionCategory>()
            .HasOne(promotionCategory => promotionCategory.Category)
            .WithMany()
            .HasForeignKey(promotionCategory => promotionCategory.CategoryId);


        //Varation
        modelBuilder.Entity<Variation>()
            .HasOne(variation => variation.Category)
            .WithMany()
            .HasForeignKey(variation => variation.CategoryId);


        //VarationOption
        modelBuilder.Entity<VariationOption>()
            .HasOne(variationOption => variationOption.Variation)
            .WithMany()
            .HasForeignKey(variationOption => variationOption.VariationId);


        //ShoppingCart
        modelBuilder.Entity<ShoppingCart>()
            .HasOne(shoppingCart => shoppingCart.User)
            .WithMany()
            .HasForeignKey(shoppingCart => shoppingCart.UserId);


        //ShoppingCartItem
        modelBuilder.Entity<ShoppingCartItem>()
            .HasOne(shoppingCartItem => shoppingCartItem.Cart)
            .WithMany()
            .HasForeignKey(shoppingCartItem => shoppingCartItem.CartId);

        modelBuilder.Entity<ShoppingCartItem>()
            .HasOne(shoppingCartItem => shoppingCartItem.ProductItem)
            .WithMany()
            .HasForeignKey(shoppingCartItem => shoppingCartItem.ProductItemId);


        //UserAddress
        modelBuilder.Entity<UserAddress>()
            .HasOne(userAddress => userAddress.User)
            .WithMany()
            .HasForeignKey(userAddress => userAddress.UserId);

        modelBuilder.Entity<UserAddress>()
            .HasOne(userAddress => userAddress.Address)
            .WithMany()
            .HasForeignKey(userAddress => userAddress.AddressId);


        //UserReview
        modelBuilder.Entity<UserReview>()
            .HasOne(userReview => userReview.User)
            .WithMany()
            .HasForeignKey(userReview => userReview.UserId);

        modelBuilder.Entity<UserReview>()
            .HasOne(userReview => userReview.OrderLine)
            .WithMany()
            .HasForeignKey(userReview => userReview.OrderLineId);

        #endregion
    }
}
