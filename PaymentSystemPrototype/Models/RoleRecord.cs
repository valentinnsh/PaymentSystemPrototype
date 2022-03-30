using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentSystemPrototype.Models;
[Table("roles")]
public class RoleRecord
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")] 
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    
    public UserRoleRecord UserRoleRecord { get; set; }
}