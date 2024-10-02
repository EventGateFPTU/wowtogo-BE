using Domain.Responses.Responses_Category;

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
        string OrganizerDescription,
        string BackgroundImageUrl,
        string BannerImageUrl,
        string[] AdditionalImages,
        decimal FromPrice,
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
