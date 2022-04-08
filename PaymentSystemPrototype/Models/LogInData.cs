using System.ComponentModel.DataAnnotations;

namespace PaymentSystemPrototype.Models;

public class LogInData
{
    [Required(ErrorMessage = "Email is Required")]
    [EmailAddress(ErrorMessage = "Incorrect Email format")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}