using Domain.Entities.Address;
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

    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }
    public DbSet<Promotion> Promotions { get; set; }
    public DbSet<Variation> Variations { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<UserReview> UserReviews { get; set; }
    public DbSet<PaymentType> PaymentTypes { get; set; }
    public DbSet<ProductItem> ProductItems { get; set; }
    public DbSet<UserAddress> UserAddresses { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<ShippingMethod> ShippingMethods { get; set; }
    public DbSet<VarationOption> VarationOptions { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    public DbSet<PromotionCategory> PromotionCategories { get; set; }
    public DbSet<ProductConfiguration> ProductConfigurations {get; set;}


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region FluntApi

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


        #endregion
    }
}
