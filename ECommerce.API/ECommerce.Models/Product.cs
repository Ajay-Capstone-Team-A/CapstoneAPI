using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    [Table("Products", Schema = "ecd")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImage { get; set; }

        public Product() { }
        public Product(int id, string name, int quantity, decimal price, string description, string image)
        {
            this.ProductId = id;
            this.ProductName = name;
            this.ProductQuantity = quantity;
            this.ProductPrice = price;
            this.ProductDescription = description;
            this.ProductImage = image;
        }
    }
}
