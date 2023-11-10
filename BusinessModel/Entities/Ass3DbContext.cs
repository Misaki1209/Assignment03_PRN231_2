using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessModel.Entities;

public class Ass3DbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public Ass3DbContext()
    {
        
    }
    public Ass3DbContext(DbContextOptions<Ass3DbContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        var configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("Assignment3_PRN231"));
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; } 
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<OrderDetail>().HasKey(x => new { x.OrderId, x.ProductId });
        
        modelBuilder.Entity<Category>()
            .HasMany(c => c.Products)  
            .WithOne(p => p.Category)  
            .HasForeignKey(p => p.CategoryId);
        
        modelBuilder.Entity<OrderDetail>()
            .HasOne(pc => pc.Product)
            .WithMany(p => p.OrderDetails)
            .HasForeignKey(pc => pc.ProductId);

        modelBuilder.Entity<OrderDetail>()
            .HasOne(pc => pc.Order)
            .WithMany(c => c.OrderDetails)
            .HasForeignKey(pc => pc.OrderId);
        
        modelBuilder.Entity<User>()
            .HasMany(c => c.Orders)  
            .WithOne(p => p.User)  
            .HasForeignKey(p => p.MemberId);

        /*modelBuilder.Entity<IdentityUserLogin<int>>().HasNoKey();
        modelBuilder.Entity<IdentityUserRole<int>>().HasKey(x => new{x.UserId, x.RoleId});
        modelBuilder.Entity<IdentityUserToken<int>>().HasNoKey();*/
        
        modelBuilder.Entity<Product>()
            .Property(p => p.UnitPrice)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<Product>()
            .Property(p => p.Weight)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<OrderDetail>()
            .Property(od => od.UnitPrice)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<OrderDetail>()
            .Property(od => od.Discount)
            .HasColumnType("decimal(18, 2)");
    }
}