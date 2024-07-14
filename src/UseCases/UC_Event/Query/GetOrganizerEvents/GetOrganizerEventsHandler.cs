using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Responses.Responses_Event;
using MediatR;
using UseCases.Common.Models;

namespace UseCases.UC_Event.Query.GetOrganizerEvents;

public class GetOrganizerEventsHandler(IUnitOfWork unitOfWork, CurrentUser currentUser) : IRequestHandler<GetOrganizerEventsQuery, List<EventDB>>
{
    public async Task<List<EventDB>> Handle(GetOrganizerEventsQuery request, CancellationToken cancellationToken)
    {
        var events = await unitOfWork.EventRepository.GetOrganizerEvents(currentUser.User!.Id, cancellationToken);
        return Result.Success(events, "Success");
    }
}