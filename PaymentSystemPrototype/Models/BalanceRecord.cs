using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentSystemPrototype.Models;

[Table("balances")]
public class BalanceRecord
{
    [Column("amount")]
    public int Amount { get; set; }
    
    [Key]
    [Column("user_id")]
    public int UserId { get; set; }
    public UserRecord UserRecord { get; set; }
    
}