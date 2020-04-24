namespace Users.API.Persistence.Contexts
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using Users.API.Domain.Models;
    using Users.API.Domain.Models.Enums;

    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(u => u.Name).IsRequired().HasMaxLength(30);
            modelBuilder.Entity<User>().Property(u => u.DateOfBirth).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.AccessLevel).IsRequired();

            modelBuilder.Entity<User>().HasData
                (
                    new User { Id = 100, Name="Test Elek", DateOfBirth= new DateTime(1991,08,11), AccessLevel= UserAccessLevel.Full}
                );
        }
    }
}