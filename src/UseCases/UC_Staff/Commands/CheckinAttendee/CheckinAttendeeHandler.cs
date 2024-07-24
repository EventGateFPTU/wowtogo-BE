using Ardalis.Result;
using Domain.Enums;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_Attendee;
using MediatR;
using UseCases.Common.Constants;
using UseCases.Common.Contracts;
using UseCases.Common.Models;
using UseCases.Mapper.Mapp_Attendee;

namespace UseCases.UC_Staff.Commands.CheckinAttendee;
public class CheckinAttendeeHandler(IUnitOfWork unitOfWork, CurrentUser currentUser, IPermissionManager permissionManager) : IRequestHandler<CheckinAttendeeCommand, Result<AttendeeDetailResponse>>
{
    public async Task<Result<AttendeeDetailResponse>> Handle(CheckinAttendeeCommand request, CancellationToken cancellationToken)
    {
        Ticket? ticket = await unitOfWork.TicketRepository.GetTicketDetailByCode(request.Code, request.ShowId, trackChanges: true, cancellationToken: cancellationToken);
        if (ticket is null) return Result.NotFound("Ticket is not found");

        Checkin? checkin = await unitOfWork.CheckinRepository.FindAsync(c => c.TicketId.Equals(ticket.Id) || c.ShowId.Equals(request.ShowId), cancellationToken: cancellationToken);
        if (checkin is not null) return Result.Success(ticket.Attendee.MapToAttendeeDetailResponse(ticket), "Ticket is already checked in");

        // TODO: When already checked-in, return attendee info

        if (!Enum.TryParse(request.UsedInFormat, out UsedInFormatEnum usedInFormatEnum))
            return Result.Error("Invalid used in format");

        if (!await CurrentUserCanCheckInTicket(ticket.Id))
            return Result.Error("The current user can not check-in for this ticket");

        if (!await TicketCanPassShow(ticketId: ticket.Id, showId: request.ShowId))
            return Result.Forbidden();

        // ticket.CheckIn(usedInFormatEnum);
        var newCheckIn = new Checkin
        {
            ShowId = request.ShowId,
            TicketId = ticket.Id
        };
        unitOfWork.CheckinRepository.Add(newCheckIn);

        if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to check in ticket");

        var result = ticket.Attendee.MapToAttendeeDetailResponse(ticket);
        return Result.Success(result, "Ticket is checked in successfully");
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