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
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    [Column("last_name")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
    [Column("email")]
    [Display(Name = "Email")]
    public string Email { get; set; }
    [Column("password")]
    public string Password { get; set; } // TODO Not a good practice. Change asap
    [Column("registered_at")]
    [Display(Name = "Register Date")]
    public DateTime RegisteredAt { get; set; }
    
    public BalanceRecord BalanceRecord { get; set; }
    public UserRoleRecord UserRoleRecord { get; set; }
    public VereficationRecord VereficationRecord { get; set; }
}