using System.ComponentModel.DataAnnotations;

namespace PaymentSystemPrototype.Models;

public class SignUpData
{
    [Required(ErrorMessage = "Required")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$",ErrorMessage = "Invalid First Name format")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "Required")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$",ErrorMessage = "Invalid Last Name format")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "Required")]
    [EmailAddress(ErrorMessage = "Invalid Email address")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Required")]
    public string Password { get; set; }
}