using System.ComponentModel.DataAnnotations;

namespace PaymentSystemPrototype.Models;

public class UserRecord
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime RegisteredAt { get; set; }
    
}