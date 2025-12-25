using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AspnetCoreMvcFull.Models;

namespace AspnetCoreMvcFull.Data
{
  public class ApplicationDbContext : IdentityDbContext // Giữ nguyên nếu dự án sử dụng ASP.NET Core Identity
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<ProductImage> ProductImages { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Cấu hình quan hệ Category → Products
      modelBuilder.Entity<Category>()
          .HasMany(c => c.Products)
          .WithOne(p => p.Category)
          .HasForeignKey(p => p.CategoryId)
          .OnDelete(DeleteBehavior.Cascade);

      // Cấu hình quan hệ Product → ProductImages
      modelBuilder.Entity<Product>()
          .HasMany(p => p.ProductImages)
          .WithOne(pi => pi.Product)
          .HasForeignKey(pi => pi.ProductId)
          .OnDelete(DeleteBehavior.Cascade);

      // *** Quan trọng: Chỉ định tên bảng thực tế trong database là "ProductImage" (số ít) ***
      modelBuilder.Entity<ProductImage>(entity =>
      {
        entity.ToTable("ProductImage"); // Tên bảng chính xác trong SQL Server

        // Index đảm bảo chỉ có một ảnh chính (IsMain = true) cho mỗi sản phẩm
        entity.HasIndex(pi => new { pi.ProductId, pi.IsMain })
              .HasFilter("\"IsMain\" = 1") // SQL Server dùng 1/0 cho BIT, hoặc có thể dùng true
              .IsUnique();

        // Index hỗ trợ sắp xếp theo SortOrder
        entity.HasIndex(pi => new { pi.ProductId, pi.SortOrder });
      });
    }
  }
}
