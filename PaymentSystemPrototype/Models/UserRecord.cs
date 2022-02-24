using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentSystemPrototype.Models;

public class UserRecord
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; } // TODO Not a good practice. Change asap
    public DateTime RegisteredAt { get; set; }
    
    // public UserRecord(Dictionary<string, string> userData)
    // {
    //     FirstName = userData["FirstName"];
    //     LastName = userData["LastName"];
    //     Email = userData["Email"];
    //     Password = userData["Password"];
    //     RegisteredAt = DateTime.Now;
    // }
}