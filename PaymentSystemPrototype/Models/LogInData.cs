using System.ComponentModel.DataAnnotations;

namespace PaymentSystemPrototype.Models;

public class LogInData
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}