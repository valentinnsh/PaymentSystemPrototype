using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentSystemPrototype.Models;
[Table("user_roles")]
public class UserRoleRecord
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("user_id")]
    public int UserId { get; set; }
    public UserRecord UserRecord { get; set; }
    
    [Column("role_id")]
    public int RoleId { get; set; }
    public RoleRecord RoleRecord { get; set; }
}