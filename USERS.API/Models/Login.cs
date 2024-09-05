using System.ComponentModel.DataAnnotations;

namespace USERS.API.Models;

public class Login
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Lembra de mim?")]
    public bool RememberMe { get; set; }
}
