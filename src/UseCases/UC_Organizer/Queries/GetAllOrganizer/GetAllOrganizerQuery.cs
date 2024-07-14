using Ardalis.Result;
using Domain.Responses.Responses_Organizer;
using Domain.Responses.Shared;
using MediatR;

namespace UseCases.UC_Organizer.Queries.GetAllOrganizer
{
    public record GetAllOrganizerQuery(int PageNumber = 1, int PageSize = 10, bool trackChanges = false, string? SearchTerm = null) : IRequest<Result<PaginatedResponse<OrganizerDB>>>;
}
