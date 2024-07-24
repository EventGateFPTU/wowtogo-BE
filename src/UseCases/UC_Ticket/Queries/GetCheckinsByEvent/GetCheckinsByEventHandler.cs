using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_Checkin;
using Domain.Responses.Shared;
using MediatR;
using UseCases.Common.Models;

namespace UseCases.UC_Ticket.Queries.GetCheckinsByEvent;
public class GetCheckinsByEventHandler(IUnitOfWork unitOfWork, CurrentUser currentUser) : IRequestHandler<GetCheckinsByEventQuery, Result<PaginatedResponse<GetCheckinDetailResponse>>>
{
    public async Task<Result<PaginatedResponse<GetCheckinDetailResponse>>> Handle(GetCheckinsByEventQuery request, CancellationToken cancellationToken)
    {
        Event? checkingEvent = await unitOfWork.EventRepository.GetEventWithOrganizer(request.EventId, cancellationToken: cancellationToken);
        if (checkingEvent is null) return Result.NotFound("Event is not found !");
        if (!IsStaffOrOrganizer(checkingEvent)) return Result.Forbidden();
        PaginatedResponse<GetCheckinDetailResponse> gettingTickets = await unitOfWork.CheckinRepository.GetCheckinsByEventAsync(eventId: request.EventId,
                                                                                                                                            pageNumber: request.PageNumber,
                                                                                                                                            pageSize: request.PageSize,
                                                                                                                                            cancellationToken: cancellationToken);
        return Result.Success(gettingTickets, "Get Checkin Successfully!");
    }
    private bool IsStaffOrOrganizer(Event checkingEvent)
        => checkingEvent.Staffs.Any(s => s.Id.Equals(currentUser.User!.Id)) || checkingEvent.Organizer.Id.Equals(currentUser.User!.Id);
}