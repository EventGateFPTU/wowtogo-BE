using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_Show;
using MediatR;

namespace UseCases.UC_Show.Queries.GetShowsOfEvent;
public class GetShowsOfEventHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetShowsOfEventQuery, Result<GetShowsOfEventResponse>>
{
    public async Task<Result<GetShowsOfEventResponse>> Handle(GetShowsOfEventQuery request, CancellationToken cancellationToken = default)
    {
        Event? checkingEvent = await unitOfWork.EventRepository.FindAsync(e => e.Id.Equals(request.EventId), cancellationToken: cancellationToken);
        if (checkingEvent is null) return Result.NotFound("Event is not found");
        IEnumerable<GetShowDetailResponse> show = await unitOfWork.ShowRepository
            .GetShowsOfEventAsync(
                eventId: request.EventId,
                pageNumber: request.PageNumber,
                pageSize: request.PageSize,
            cancellationToken: cancellationToken);
        return Result.Success(new GetShowsOfEventResponse(
            pageNumber: request.PageNumber,
            pageSize: request.PageSize,
            Shows: show
        ), "Get shows of event successfully");
    }
}