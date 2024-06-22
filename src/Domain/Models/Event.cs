using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;
using Domain.Models.Shared;

namespace Domain.Models;
public class Event : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public EventStatusEnum Status { get; set; } = EventStatusEnum.Draft;
    public required Guid OrganizerId { get; set; }
    public int MaxTickets { get; set; } = 0;
    //----------------------------------------- 
    [ForeignKey(nameof(OrganizerId))]
    public Organizer Organizer { get; set; } = null!;
    public ICollection<Show> Shows { get; set; } = [];
    public ICollection<Staff> Staffs { get; set; } = [];
    public ICollection<EventCategory> EventCategories { get; set; } = [];
    public ICollection<Attendee> Attendees { get; set; } = [];
}