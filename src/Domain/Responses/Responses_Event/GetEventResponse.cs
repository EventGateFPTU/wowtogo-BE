using Domain.Responses.Responses_Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Responses.Responses_Event
{
    public record GetEventResponse(
        Guid Id,
        string Title,
        string Description,
        string Location,
        CategoryDB[] Categories,
        string Status,
        string OrganizerName,
        string OrganizerImageUrl,
        string BackgroundImageUrl,
        string BannerImageUrl,
        DateTimeOffset CreatedAt);
    public record EventDB(Guid Id, string Title, 
        string Description,
        string Location,
        string Status,
        string OrganizerName,
        int MaxTickets,
        DateTimeOffset CreatedAt);
    public record GetAllEventsResponse(int PageNumber, int PageSize, IEnumerable<EventDB> Events);
}   
