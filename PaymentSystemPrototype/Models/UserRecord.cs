using System.ComponentModel.DataAnnotations;

namespace PaymentSystemPrototype.Models;

public class UserRecord
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; } // TODO Not a good practice. Change asap
    public DateTime RegisteredAt { get; set; }
    
}