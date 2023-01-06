using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    [Table("Reviews", Schema = "ecd")]
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }

        public Review() { }

        public Review(int reviewId,int userId,int productId,string comment,int rating) {
            this.ReviewId = reviewId;
            this.UserId = userId;
            this.ProductId = productId;
            this.Comment = comment;
            this.Rating = rating;
        }
        
        //constructor w/o reviewID
        public Review(int userId, int productId,string comment,int rating)
        {
            this.UserId = userId;
            this.ProductId = productId;
            this.Comment = comment;
            this.Rating = rating;
        }

    }
}
