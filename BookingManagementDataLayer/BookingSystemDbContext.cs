using BookingManagementDataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookingManagementDataLayer
{
    public class BookingSystemDbContext : DbContext
    {
        public BookingSystemDbContext(DbContextOptions<BookingSystemDbContext> options) : base(options) { }

        public DbSet<MemberInfo> Members { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<BookingDetails> BookingDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MemberInfo>().ToTable("MemberInfo"); // Explicitly map to "Members"
            modelBuilder.Entity<Inventory>().ToTable("Inventory"); // If your table name is "Inventory"
            modelBuilder.Entity<BookingDetails>().ToTable("BookingDetails");


            modelBuilder.Entity<BookingDetails>()
                .HasOne(b => b.Member)
                .WithMany()
                .HasForeignKey(b => b.MemberId);

            modelBuilder.Entity<BookingDetails>()
                .HasOne(b => b.Inventory)
                .WithMany()
                .HasForeignKey(b => b.InventoryId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("Data Source=.;Initial Catalog=BookingSystem;Integrated Security=True;TrustServerCertificate=True");
            }
        }
    }

}
