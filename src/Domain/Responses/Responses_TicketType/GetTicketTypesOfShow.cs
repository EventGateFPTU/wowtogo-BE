using Domain.Models;

namespace Domain.Responses.Responses_TicketType;
public record GetTicketTypesOfShowResponse(
    IEnumerable<GetTicketTypeDetailsResponse> TicketTypes,
    int PageSize,
    int PageNumber,
    int MaxPage
);