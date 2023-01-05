using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//just a class to hold profile info
namespace ECommerce.Models
{
    public class ProfileDTO
    {
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }

        public ProfileDTO(string userFirstName, string userLastName, string userEmail)
        {
            UserFirstName = userFirstName;
            UserLastName = userLastName;
            UserEmail = userEmail;
        }
    }
}
