using Domain.Models;

namespace API.Authorization;

public class CurrentUser
{
    public required User user { get; set; }
    
}