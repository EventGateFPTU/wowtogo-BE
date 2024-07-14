using Ardalis.Result;
using Domain.Enums;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_Attendee;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenFga.Sdk.ApiClient;
using UseCases.Common.Constants;
using UseCases.Common.Contracts;
using UseCases.Common.Models;
using UseCases.Mapper.Mapp_Attendee;

namespace UseCases.UC_Staff.Commands.Checkin;
public class CheckinHandler(IUnitOfWork unitOfWork, CurrentUser currentUser, IPermissionManager permissionManager) : IRequestHandler<CheckinCommand, Result<AttendeeDetailResponse>>
{
    public async Task<Result<AttendeeDetailResponse>> Handle(CheckinCommand request, CancellationToken cancellationToken)
    {
        Ticket? ticket = await unitOfWork.TicketRepository.GetTicketDetailByCode(request.Code, request.ShowId, trackChanges: true, cancellationToken: cancellationToken);
        if (ticket is null) return Result.NotFound("Ticket is not found");
        if (ticket.IsCheckedIn()) return Result.Error("Ticket is already checked in");
        if (!Enum.TryParse(request.UsedInFormat, out UsedInFormatEnum usedInFormatEnum))
            return Result.Error("Invalid used in format");

        if (!await CurrentUserCanCheckInTicket(ticket.Id))
            return Result.Error("The current user can not check-in for this ticket");
        
        if (!await TicketCanPassShow(ticketId: ticket.Id, showId: request.ShowId))
            return Result.Forbidden();
        
        ticket.CheckIn(usedInFormatEnum);
        var newCheckIn = new Domain.Models.Checkin
        {
            ShowId = request.ShowId,
            TicketId = ticket.Id
        };
        unitOfWork.CheckinRepository.Add(newCheckIn);
        
        if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to check in ticket");

        var result = ticket.Attendee.MapToAttendeeDetailResponse(ticket);
        var attendee = await unitOfWork.AttendeeRepository
            .FindAsync(a => a.Id.Equals(ticket.AttendeeId), cancellationToken: cancellationToken);
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