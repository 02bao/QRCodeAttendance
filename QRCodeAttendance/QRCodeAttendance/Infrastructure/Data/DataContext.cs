using Microsoft.EntityFrameworkCore;
using QRCodeAttendance.QRCodeAttendance.Domain.Entities;

namespace QRCodeAttendance.QRCodeAttendance.Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<SqlUser> Users { get; set; }
    public DbSet<SqlRole> Roles { get; set; }
    public DbSet<SqlToken> Tokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // setup 1 user có nhiều role và 1 role có nhiều user
        modelBuilder.Entity<SqlUser>()
            .HasOne(x => x.Role)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.RoleId);

        // setup seed data cho role 
        modelBuilder.Entity<SqlRole>()
            .HasData(
                new SqlRole { Id = 1, Name = "Admin" },
                new SqlRole { Id = 2, Name = "User" });
        // setup seed data cho user
        modelBuilder.Entity<SqlUser>()
            .HasData(
                new SqlUser
                {
                    Id = 1,
                    Email = "admin@gmail.com",
                    Password = "admin",
                    FullName = "Admin",
                    RoleId = 1
                });
    }
}