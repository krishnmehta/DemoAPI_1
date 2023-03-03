using customerDemoWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace customerDemoWebAPI.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
    }
}
