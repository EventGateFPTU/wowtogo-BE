using Ardalis.Result;
using Domain.Responses.Responses_Event;
using Domain.Responses.Shared;
using MediatR;

namespace UseCases.UC_Event.Query.GetEventOfCurrentStaff;
public record GetEventOfCurrentStaffQuery(
    int PageSize,
    int PageNumber
) : IRequest<Result<PaginatedResponse<GetEventResponse>>>;