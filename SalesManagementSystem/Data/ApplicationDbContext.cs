using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Data
{
    // Make Repository for each table access
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions options)
            :base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<WorksWith> WorksWiths { get; set; }
        public DbSet<Transaction> TransactionHistories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Branch)
                .WithMany()
                .HasForeignKey(e => e.BranchId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Branch>()
                .HasOne(b => b.Manager)
                .WithOne()
                .HasForeignKey<Branch>(b => b.ManagerId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Transaction>(entity => entity
                .HasIndex(e => e.IdempotencyKey)
                .IsUnique()
            );

            List<IdentityRole> roles = new List<IdentityRole>
                {
                    new IdentityRole 
                    {
                        Id = "c69d7990-ae54-4b39-b4a0-9c110d3ff19e",
                        ConcurrencyStamp = "088a455c-0d26-4533-9eb3-90b1275e0d27",
                        Name = "Admin", 
                        NormalizedName = "ADMIN"
                    },
                    new IdentityRole 
                    { 
                        Id = "9c5e6baa-d8fb-4ad4-bf47-05f624a509a4",
                        ConcurrencyStamp = "d1f25502-da5a-4572-8653-d68a0e7c1caf",
                        Name = "User", 
                        NormalizedName = "USER" 
                    }
                };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }

    }
