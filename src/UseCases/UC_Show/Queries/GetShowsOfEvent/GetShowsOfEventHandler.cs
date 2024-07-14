using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_Show;
using Domain.Responses.Shared;
using MediatR;

namespace UseCases.UC_Show.Queries.GetShowsOfEvent;
public class GetShowsOfEventHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetShowsOfEventQuery, Result<PaginatedResponse<GetShowDetailResponse>>>
{
    public async Task<Result<PaginatedResponse<GetShowDetailResponse>>> Handle(GetShowsOfEventQuery request, CancellationToken cancellationToken = default)
    {
        Event? checkingEvent = await unitOfWork.EventRepository.FindAsync(e => e.Id.Equals(request.EventId), cancellationToken: cancellationToken);
        if (checkingEvent is null) return Result.NotFound("Event is not found");
        PaginatedResponse<GetShowDetailResponse> show = await unitOfWork.ShowRepository
            .GetShowsOfEventAsync(
                eventId: request.EventId,
                pageNumber: request.PageNumber,
                pageSize: request.PageSize,
            cancellationToken: cancellationToken);
        return Result.Success(show, "Get shows of event successfully");
    }
}