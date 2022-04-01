using System.ComponentModel.DataAnnotations;

namespace PaymentSystemPrototype.Models;

public class TransferData
{
    [Required]
    public long CardNumber { get; set; }
    public int? ExpireDate { get; set; }
    public string? CardHolder { get; set; }
    public int? CV2 { get; set; }
    [Required]
    public int Amount { get; set; }
}