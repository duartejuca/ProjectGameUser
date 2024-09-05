

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace USERS.API.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        
        [PasswordPropertyText]
        public string Password { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        
    }
}