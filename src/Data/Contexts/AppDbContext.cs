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
        #region Filter

            modelBuilder.Entity<Address>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Country>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<District>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Region>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Attachment>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Order>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<OrderLine>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<OrderStatus>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<ShippingMethod>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<PaymentMethod>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<PaymentType>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Category>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Product>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<ProductAttachment>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<ProductConfiguration>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<ProductItem>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<ProductItemAttachment>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Promotion>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<PromotionCategory>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Variation>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<VariationOption>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<ShoppingCart>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<ShoppingCartItem>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<UserAddress>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<UserReview>().HasQueryFilter(u => !u.IsDeleted);

        #endregion
    }
}
