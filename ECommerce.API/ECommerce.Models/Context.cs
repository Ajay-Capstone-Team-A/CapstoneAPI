using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class Context : DbContext,IContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }
        public DbSet<Product> Product { get; set; } = null!;
        public DbSet<User> User { get; set; } = null!;

        public Task SaveAsync() { return SaveChangesAsync(); }
    }
}
