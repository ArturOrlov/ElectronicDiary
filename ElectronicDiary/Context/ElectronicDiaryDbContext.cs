using ElectronicDiary.Entities.DbModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ElectronicDiary.Context;

public class ElectronicDiaryDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
    IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public ElectronicDiaryDbContext(DbContextOptions<ElectronicDiaryDbContext> options) : base(options)
    {
    }

    private const string IdentitySchema = "Identity";
    private const string SchoolSchema = "School";

    public DbSet<Role> Role { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<UserRole> UserRole { get; set; }
    public DbSet<UserInfo> UserInfo { get; set; }
    public DbSet<UserClass> UserClass { get; set; }
    public DbSet<SchoolClass> SchoolClass { get; set; }
    public DbSet<Homework> Homework { get; set; }
    public DbSet<Subject> Subject { get; set; }
    public DbSet<Cabinet> Cabinet { get; set; }
    public DbSet<Timetable> Timetable { get; set; }
    public DbSet<PerformanceRating> PerformanceRating { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<Role>(entity =>
        {
            entity.ToTable("Role", IdentitySchema);
            entity.Property(p => p.CreatedAt).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
            entity.Property(p => p.UpdatedAt).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
        });
        
        builder.Entity<User>(entity =>
        {
            entity.ToTable("User", IdentitySchema);
            entity.Property(p => p.CreatedAt).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
            entity.Property(p => p.UpdatedAt).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
        });
        
        builder.Entity<UserRole>(entity =>
        {
            entity.ToTable("UserRole", IdentitySchema);
            entity.Property(p => p.CreatedAt).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
            entity.Property(p => p.UpdatedAt).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
        });
        
        builder.Entity<UserInfo>(entity =>
        {
            entity.ToTable("UserInfo", IdentitySchema);
            entity.Property(p => p.CreatedAt).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
            entity.Property(p => p.UpdatedAt).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
        });
        
        builder.Entity<UserClass>(entity =>
        {
            entity.ToTable("UserClass", IdentitySchema);
            entity.Property(p => p.CreatedAt).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
            entity.Property(p => p.UpdatedAt).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
        });
        
        builder.Entity<SchoolClass>(entity =>
        {
            entity.ToTable("Class", SchoolSchema);
            entity.Property(p => p.CreatedAt).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
            entity.Property(p => p.UpdatedAt).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
        });

        builder.Entity<Homework>(entity =>
        {
            entity.ToTable("Homework", SchoolSchema);
            entity.Property(p => p.CreatedAt).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
            entity.Property(p => p.UpdatedAt).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
        });

        builder.Entity<Subject>(entity =>
        {
            entity.ToTable("Subject", SchoolSchema);
            entity.Property(p => p.CreatedAt).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
            entity.Property(p => p.UpdatedAt).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
        });
        
        builder.Entity<Cabinet>(entity =>
        {
            entity.ToTable("Cabinet", SchoolSchema);
            entity.Property(p => p.CreatedAt).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
            entity.Property(p => p.UpdatedAt).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
        });

        builder.Entity<Timetable>(entity =>
        {
            entity.ToTable("Timetable", SchoolSchema);
            entity.Property(p => p.CreatedAt).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
            entity.Property(p => p.UpdatedAt).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
        });
        
        builder.Entity<PerformanceRating>(entity =>
        {
            entity.ToTable("PerformanceRating", SchoolSchema);
            entity.Property(p => p.CreatedAt).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
            entity.Property(p => p.UpdatedAt).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
        });
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // todo понять как передавать параметр из аппсетингс
        optionsBuilder.UseNpgsql("Host=192.168.0.2;port=5433;Database=ElectronicDiary;Username=postgres;Password=123;Include Error Detail=true");
    }
}