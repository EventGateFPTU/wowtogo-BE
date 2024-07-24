using Ardalis.Result;
using Domain.Enums;
using Domain.Interfaces.Data;
using Domain.Responses.Responses_Attendee;
using MediatR;
using UseCases.Common.Constants;
using UseCases.Common.Contracts;
using UseCases.Common.Models;

namespace UseCases.UC_Staff.Commands.CheckinAttendee;
public class CheckinAttendeeHandler(IUnitOfWork unitOfWork, CurrentUser currentUser, IPermissionManager permissionManager) : IRequestHandler<CheckinAttendeeCommand, Result<AttendeeDetailResponse>>
{
    public async Task<Result<AttendeeDetailResponse>> Handle(CheckinAttendeeCommand request, CancellationToken cancellationToken)
    {
        var ticket = await unitOfWork.TicketRepository.FindAsync(x => x.Id == request.TicketId, trackChanges: true, cancellationToken: cancellationToken);
        if (ticket is null) return Result.NotFound("Ticket is not found");


        if (!Enum.TryParse(request.UsedInFormat, out UsedInFormatEnum usedInFormatEnum))
            return Result.Error("Invalid used in format");

        if (!await CurrentUserCanCheckInTicket(ticket.Id))
            return Result.Error("The current user can not check-in for this ticket");

        if (!await TicketCanPassShow(ticketId: ticket.Id, showId: request.ShowId))
            return Result.Forbidden();

        // ticket.CheckIn(usedInFormatEnum);
        var newCheckIn = new Domain.Models.Checkin
        {
            ShowId = request.ShowId,
            TicketId = ticket.Id
        };
        unitOfWork.CheckinRepository.Add(newCheckIn);

        if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to check in ticket");

        return Result.Success();
    }

    private async Task<bool> TicketCanPassShow(Guid ticketId, Guid showId)
    {
        var ticketObj = RelationObjects.Ticket(ticketId.ToString());
        var showObj = RelationObjects.Show(showId.ToString());
        return await permissionManager.HasPermission(
            ticketObj,
            Relations.CanCheckInShow,
            showObj
        );
    }

    private async Task<bool> CurrentUserCanCheckInTicket(Guid ticketId)
    {
        var currentUserObj = RelationObjects.User(currentUser.User!.Id.ToString());
        var ticketObj = RelationObjects.Ticket(ticketId.ToString());
        return await permissionManager.HasPermission(
            currentUserObj,
            Relations.CanCheckInTicket,
            ticketObj
        );
    }
}