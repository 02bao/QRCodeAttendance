using Microsoft.EntityFrameworkCore;
using QRCodeAttendance.Domain.Entities;

namespace QRCodeAttendance.Infrastructure.Data;

public class DataContext : DbContext
{
    //add-migration daylaMessage -OutputDir Infrastructure\Migrations
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<SqlUser> Users { get; set; }
    public DbSet<SqlRole> Roles { get; set; }
    public DbSet<SqlToken> Tokens { get; set; }
    public DbSet<SqlDepartment> Departments { get; set; }
    public DbSet<SqlPosition> Positions { get; set; }
    public DbSet<SqlFile> Files { get; set; }
    public DbSet<SqlCompany> Companies { get; set; }

    public Random random = new();
    public string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<SqlUser>()
        //    .HasOne(x => x.Role)
        //    .WithMany(x => x.Users)
        //    .HasForeignKey(x => x.RoleId);

        //modelBuilder.Entity<SqlUser>()
        //    .HasOne(x => x.Position)
        //    .WithMany(x => x.Users)
        //    .IsRequired(false);

        modelBuilder.Entity<SqlRole>()
            .HasData(
                new SqlRole { Id = 1, Name = "Admin" },
                new SqlRole { Id = 2, Name = "User" });
        modelBuilder.Entity<SqlUser>()
            .HasData(
                new SqlUser
                {
                    Id = 1,
                    Email = "admin@gmail.com",
                    Password = "admin",
                    FullName = "Admin",
                    Images = null,
                    IsVerified = true,
                    RoleId = 1
                });


    }

}