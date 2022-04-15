using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace PaymentSystemPrototype.Models;

public class TransferData
{
    [Required(ErrorMessage = "required")]
    // Ideally here I should've used more complex attribute. But for simplicity of testing - credit card number 
    // is just a 16-dijit number
    [Range(1000000000000000, 9999999999999999, ErrorMessage = "Invalid Card Number")]
    public long CardNumber { get; set; }
    [Required(ErrorMessage = "required")]
    public DateTime? ExpireDate { get; set; }
    [Required(ErrorMessage = "required")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$",ErrorMessage = "Invalid Last Name format")]
    public string? CardHolder { get; set; }
    [Required(ErrorMessage = "required")]
    [Range(100,999, ErrorMessage = "Invalid CV2")]
    public int? CV2 { get; set; }
    [Required(ErrorMessage = "required")]
    public decimal Amount { get; set; }
}