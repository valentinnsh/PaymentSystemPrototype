using System.ComponentModel.DataAnnotations;

namespace PaymentSystemPrototype.Models;

public class WithdrawalData
{
    [Required(ErrorMessage = "required")]
    // Ideally here I should've used more complex attribute. But for simplicity of testing - credit card number 
    // is just a 16-dijit number
    [DataType(DataType.CreditCard)]
    [Range(1000000000000000, 9999999999999999, ErrorMessage = "Invalid Card Number")]
    public long CardNumber { get; set; }
    [Required(ErrorMessage = "required")]
    public decimal Amount { get; set; }
}