using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentSystemPrototype.Models;

[Table("fund_transfers")]
public class TransferRecord
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("user_id")]
    public int UserId { get; set; }
    public UserRecord UserRecord { get; set; }
    
    [Column("card_number")]
    public long CardNumber { get; set; }
    
    [Column("confirmed_by")]
    public int? ConfirmedBy { get; set; }
    public UserRecord? FundsUserRecord { get; set; }

    [Column("created_at")] 
    public DateTime CreatedAt { get; set; }
    
    [Column("confirmed_at")]
    public DateTime? ConfirmedAt { get; set; }
    
    [Column("amount")]
    public int Amount { get; set; }
    [Column("status")]
    public int Status { get; set; }
    
}