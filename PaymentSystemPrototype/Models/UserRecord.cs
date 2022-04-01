using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentSystemPrototype.Models;

[Table("users")]
public class UserRecord
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    [Column("first_name")]
    public string FirstName { get; set; }
    [Column("last_name")]
    public string LastName { get; set; }
    [Column("email")]
    public string Email { get; set; }
    [Column("password")]
    public string Password { get; set; }
    [Column("registered_at")]
    public DateTime RegisteredAt { get; set; }
    
    [Column("block")]
    [DefaultValue(false)]
    public bool Block { get; set; }
    public BalanceRecord BalanceRecord { get; set; }
    public UserRoleRecord UserRoleRecord { get; set; }
    public VereficationRecord VereficationRecord { get; set; }
    
    public IEnumerable<TransferRecord> TransferRecords { get; set; }
    
    public IEnumerable<TransferRecord> ManagerTransferRecords{ get; set; }
}