using System.ComponentModel.DataAnnotations;

namespace PaymentSystemPrototype.Models;

public class LogInData
{
    public string Email { get; set; }
    public string Password { get; set; }
}