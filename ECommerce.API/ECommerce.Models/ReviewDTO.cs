using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class ReviewDTO
    {
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }

        public int ProductId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }

        public ReviewDTO(string UserFirstName, string UserLastName, int ProductId, string Comment, int Rating)
        {
            this.UserFirstName = UserFirstName;
            this.UserLastName = UserLastName;
            this.ProductId = ProductId;
            this.Comment = Comment;
            this.Rating = Rating;
        }
    }
}
