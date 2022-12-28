using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    [Table("Users", Schema = "ecd")]

    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }

        public User() { }
        public User(int UserId, string UserFirstName, string UserLastName, string UserEmail, string UserPassword)
        {
            this.UserId = UserId;
            this.UserFirstName = UserFirstName;
            this.UserLastName = UserLastName;
            this.UserEmail = UserEmail;
            this.UserPassword = UserPassword;   
            
        }
        public User(string UserFirstName, string UserLastName, string UserEmail, string UserPassword)
        {
            this.UserFirstName = UserFirstName;
            this.UserLastName = UserLastName;
            this.UserEmail = UserEmail;
            this.UserPassword = UserPassword;

        }
    }
}