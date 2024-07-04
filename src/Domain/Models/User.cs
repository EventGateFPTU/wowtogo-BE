using Domain.Models.Shared;
namespace Domain.Models;
public class User : UserProfile
{
    public required string Subject {
        get;
        set;
    }
    // public string Password { get; set; } = string.Empty;
    //----------------------------------------- 
}