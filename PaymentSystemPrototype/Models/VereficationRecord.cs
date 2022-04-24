using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentSystemPrototype.Models;

[Table("verefications")]
public class VereficationRecord
{
    [Key]
    [Column("user_id")] 
    public int UserId { get; set; }
    public UserRecord UserRecord { get; set; }
    
    [Column("status")]
    public int Status { get; set; }
    
    [Column("last_change_date")]
    public DateTime LastChangeDate { get; set; }
    
    [Column("reviewer")]
    public int? Reviewer { get; set; }
    public UserRecord? ReviewerRecord { get; set; }
}