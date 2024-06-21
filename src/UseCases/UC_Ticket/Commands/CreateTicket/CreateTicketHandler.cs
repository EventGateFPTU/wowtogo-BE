using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;
using UseCases.Utils;

namespace UseCases.UC_Ticket.Commands.CreateTicket;
public class CreateTicketHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateTicketCommand, Result<Ticket>>
{
    public async Task<Result<Ticket>> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        Ticket ticket = new()
        {
            TicketTypeId = request.TicketTypeId,
            AttendeeId = request.AttendeeId,
            Code = GenerateRandom6CharsCode.Generate(),
        };
        unitOfWork.TicketRepository.Add(ticket);
        if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to create ticket");
        return Result.Success(ticket);
    }
}