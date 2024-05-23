using lab09_cc.Model;
using Microsoft.EntityFrameworkCore;
namespace lab09_cc.DBContext
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) :base(options)        
        { 
        }
        public DbSet<Product> Products { get; set;}
    }
}
