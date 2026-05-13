using Microsoft.EntityFrameworkCore;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            :base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
