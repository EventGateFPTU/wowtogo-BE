using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Shared;

namespace Domain.Models;
public class Attendee : UserProfile
{
    public required string PhoneNumber { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public required Guid EventId { get; set; }
    public Guid? UserId { get; set; } = null!;
    //----------------------------------------- 
    [ForeignKey(nameof(EventId))]
    public Event Event { get; set; } = null!;
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
}