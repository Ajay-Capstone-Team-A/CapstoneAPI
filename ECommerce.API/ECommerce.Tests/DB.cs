using ECommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Tests
{
    public class DB 
    {
        public Context context { get; set; }
        public DB() {
            var db = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "DB").Options;
            
            context = new Context(db);

            context.Add(new User {UserId=1,UserFirstName="first",UserLastName="last",UserEmail="email",UserPassword="password"});
            context.Add(new Product {ProductId=1,ProductName="test",ProductDescription="test",ProductQuantity=1,ProductPrice=10,ProductImage="image"});
            
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
            //context.SaveChanges();
        }
    }
}
