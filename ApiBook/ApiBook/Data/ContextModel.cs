using ApiBook.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiBook.Data
{
    public class ContextModel : DbContext
    {
        public ContextModel(DbContextOptions<ContextModel> options)
            : base(options)
        {

        }
        public DbSet<UserModel> User { get; set; }
        public DbSet<BooksModel> Books { get; set; }
        public DbSet<OrdersModel> Orders { get; set; }
        public DbSet<OrderDetailsModel> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<OrderDetailsModel>()
                .HasOne(od => od.Order)
                .WithMany()
                .HasForeignKey(od => od.IdOrder);

            modelBuilder.Entity<OrderDetailsModel>()
                .HasOne(od => od.Book)
                .WithMany()
                .HasForeignKey(od => od.IdBook);

            modelBuilder.Entity<OrdersModel>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.IdUser);
        }
    }
}
