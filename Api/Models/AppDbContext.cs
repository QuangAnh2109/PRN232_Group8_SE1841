using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Xml.Linq;
using Api.Constants;

namespace Api.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Token> Tokens { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Class> Classes { get; set; } = null!;
        public DbSet<Timesheet> Timesheets { get; set; } = null!;
        public DbSet<Center> Centers { get; set; } = null!;
        public DbSet<Attendance> Attendance { get; set; } = null!;

        public DbSet<ClassStudent> ClassStudents { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClassStudent>()
                .HasKey(cu => new { cu.ClassId, cu.StudentId });

            modelBuilder.Entity<User>()
                .HasIndex(User => new { User.Username, User.IsDeleted })
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(User => new { User.Email, User.IsDeleted })
                .IsUnique();

            modelBuilder.Entity<Role>()
                .HasIndex(Role => new { Role.Name, Role.IsDeleted })
                .IsUnique();

            modelBuilder.Entity<Class>()
                .HasIndex(c => new { c.Name, c.CenterId, c.IsDeleted })
                .IsUnique();

            modelBuilder.Entity<Center>()
                .HasIndex(c => new { c.Name, c.IsDeleted })
                .IsUnique();

            modelBuilder.Entity<ClassStudent>()
                .HasIndex(cu => new { cu.StudentId, cu.ClassId, cu.IsDeleted})
                .IsUnique();

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(parameter, nameof(BaseEntity.IsDeleted));
                    var filter = Expression.Lambda(
                        Expression.Equal(property, Expression.Constant(false, typeof(bool?))),
                        parameter
                    );

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
                }
            }

            // Seed initial data for Role
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    RoleId = DefaultValues.AdminRoleId,
                    Name = DefaultValues.AdminRole,
                    Description = "Administrator role with full permissions for system manager",
                    PermissionLevel = 1,
                    CreatedBy = DefaultValues.SystemId,
                    UpdatedBy = DefaultValues.SystemId,
                    CreatedAt = DateTime.Parse("2025-01-01T00:00:00Z"),
                    UpdatedAt = DateTime.Parse("2025-01-01T00:00:00Z"),
                    IsDeleted = false,
                },
                new Role
                {
                    RoleId = DefaultValues.ManagerRoleId,
                    Name = DefaultValues.ManagerRole,
                    Description = "Manager role with full permissions for center manager",
                    PermissionLevel = 2,
                    CreatedBy = DefaultValues.SystemId,
                    UpdatedBy = DefaultValues.SystemId,
                    CreatedAt = DateTime.Parse("2025-01-01T00:00:00Z"),
                    UpdatedAt = DateTime.Parse("2025-01-01T00:00:00Z"),
                    IsDeleted = false,
                },
                new Role
                {
                    RoleId = DefaultValues.TeacherRoleId,
                    Name = DefaultValues.TeacherRole,
                    Description = "Teacher role with full permissions for class manager",
                    PermissionLevel = 3,
                    CreatedBy = DefaultValues.SystemId,
                    UpdatedBy = DefaultValues.SystemId,
                    CreatedAt = DateTime.Parse("2025-01-01T00:00:00Z"),
                    UpdatedAt = DateTime.Parse("2025-01-01T00:00:00Z"),
                    IsDeleted = false,
                },
                new Role
                {
                    RoleId = DefaultValues.StudentRoleId,
                    Name = DefaultValues.StudentRole,
                    Description = "Student role",
                    PermissionLevel = 3,
                    CreatedBy = DefaultValues.SystemId,
                    UpdatedBy = DefaultValues.SystemId,
                    CreatedAt = DateTime.Parse("2025-01-01T00:00:00Z"),
                    UpdatedAt = DateTime.Parse("2025-01-01T00:00:00Z"),
                    IsDeleted = false,
                }
            );

            //modelBuilder.Entity<User>().HasData(
            //    new User
            //    {
            //        UserId = 1,
            //        Username = "admin",
            //        Email = "admin@gmail.com",
            //        FullName = "Admin",
            //        PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin"),
            //        IsActive = true,
            //        RoleId = DefaultValues.AdminRoleId,
            //        CreatedBy = DefaultValues.SystemId,
            //        UpdatedBy = DefaultValues.SystemId,
            //        IsDeleted = false,
            //    },
            //    new User
            //    {
            //        UserId = 2,
            //        Username = "teacher",
            //        Email = "teacher@gmail.com",
            //        FullName = "Teacher",
            //        PasswordHash = BCrypt.Net.BCrypt.HashPassword("teacher"),
            //        IsActive = true,
            //        RoleId = DefaultValues.TeacherRoleId,
            //        CreatedBy = DefaultValues.SystemId,
            //        UpdatedBy = DefaultValues.SystemId,
            //        IsDeleted = false,
            //    },
            //    new User
            //    {
            //        UserId = 3,
            //        Username = "student",
            //        Email = "student@gmail.com",
            //        FullName = "Student",
            //        PasswordHash = BCrypt.Net.BCrypt.HashPassword("student"),
            //        IsActive = true,
            //        RoleId = DefaultValues.StudentRoleId,
            //        CreatedBy = DefaultValues.SystemId,
            //        UpdatedBy = DefaultValues.SystemId,
            //        IsDeleted = false,
            //    }
            //);
        }
    }
}
