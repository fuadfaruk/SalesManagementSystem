using Microsoft.EntityFrameworkCore;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Data
{
    // Make Repository for each table access
    public class ApplicationDbContext : DbContext
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
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<Branch>()
                    .HasOne(b => b.Manager)
                    .WithOne()
                    .HasForeignKey<Branch>(b => b.ManagerId)
                    .OnDelete(DeleteBehavior.SetNull);

                modelBuilder.Entity<Transaction>(entity => entity
                    .HasIndex(e => e.IdempotencyKey)
                    .IsUnique()
                );
        }
    }

    }
