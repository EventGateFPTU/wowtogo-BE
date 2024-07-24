using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;
using UseCases.Common.Constants;
using UseCases.Common.Contracts;
using UseCases.Common.Models;

namespace UseCases.UC_Staff.Queries.GetCheckinInfo;

public class GetCheckinInfoHandler(IUnitOfWork unitOfWork, CurrentUser currentUser, IPermissionManager permissionManager) : IRequestHandler<GetCheckinInfoQuery, Result<GetCheckinInfoResponse>>
{
    public async Task<Result<GetCheckinInfoResponse>> Handle(GetCheckinInfoQuery request, CancellationToken cancellationToken)
    {
        Ticket? ticket = await unitOfWork.TicketRepository.GetTicketDetailByCode(request.Code, request.ShowId, trackChanges: false, cancellationToken: cancellationToken);
        if (ticket is null) return Result.NotFound("Ticket is not found");

        if (!await CurrentUserCanCheckInTicket(ticket.Id)) return Result.Error("The current user can not check-in for this ticket");

        var canCheckin = await TicketCanPassShow(ticketId: ticket.Id, showId: request.ShowId);
        if (!canCheckin) return Result.Forbidden();

        var response = new GetCheckinInfoResponse(
            Id: ticket.Attendee.Id,
            Email: ticket.Attendee.Email,
            LastName: ticket.Attendee.LastName,
            FirstName: ticket.Attendee.FirstName,
            PhoneNumber: ticket.Attendee.PhoneNumber,
            DateOfBirth: ticket.Attendee.DateOfBirth,
            TicketId: ticket.Id,
            CanCheckin: canCheckin);
        return Result.Success(response);
    }
    
    private Task<bool> TicketCanPassShow(Guid ticketId, Guid showId)
    {
        var ticketObj = RelationObjects.Ticket(ticketId.ToString());
        var showObj = RelationObjects.Show(showId.ToString());
        return permissionManager.HasPermission(
            ticketObj,
            Relations.CanCheckInShow,
            showObj
        );
    }
    
    private Task<bool> CurrentUserCanCheckInTicket(Guid ticketId)
    {
        var currentUserObj = RelationObjects.User(currentUser.User!.Id.ToString());
        var ticketObj = RelationObjects.Ticket(ticketId.ToString());
        return permissionManager.HasPermission(
            currentUserObj,
            Relations.CanCheckInTicket,
            ticketObj
        );
    }
}