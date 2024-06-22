using Ardalis.Result;
using Domain.Enums;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;

namespace UseCases.UC_Staff.Commands.Checkin;
public class CheckinHandler(IUnitOfWork unitOfWork) : IRequestHandler<CheckinQuery, Result>
{
    public async Task<Result> Handle(CheckinQuery request, CancellationToken cancellationToken)
    {
        Ticket? ticket = await unitOfWork.TicketRepository.GetTicketDetailByCode(request.Code, request.ShowId, trackChanges: true, cancellationToken: cancellationToken);
        if (ticket is null) return Result.NotFound("Ticket is not found");
        if (ticket.IsCheckedIn()) return Result.Error("Ticket is already checked in");
        if (!Enum.TryParse(request.usedInFormat, out UsedInFormatEnum usedInFormatEnum)) return Result.Error("Invalid used in format");
        ticket.CheckIn(usedInFormatEnum);
        if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to check in ticket");
        return Result.SuccessWithMessage("Ticket is checked in successfully");
    }
}