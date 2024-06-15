using Domain.Models.Shared;
namespace Domain.Models;
public class User : UserProfile
{
    public string Password { get; set; } = string.Empty;
    //----------------------------------------- 
}