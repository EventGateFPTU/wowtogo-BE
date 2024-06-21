using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_Ticket;
using MediatR;
using UseCases.Utils;

namespace UseCases.UC_Ticket.Commands.CreateTicket;
public class CreateTicketHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateTicketCommand, Result<GetTicketDetailResponse>>
{
    public async Task<Result<GetTicketDetailResponse>> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        Ticket ticket = new()
        {
            TicketTypeId = request.TicketTypeId,
            AttendeeId = request.AttendeeId,
            Code = GenerateRandom6CharsCode.Generate(),
        };
        unitOfWork.TicketRepository.Add(ticket);
        if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to create ticket");
        GetTicketDetailResponse? ticketDetail = await unitOfWork.TicketRepository.GetTicketDetail(ticket.Id, cancellationToken: cancellationToken);
        if (ticketDetail == null) return Result.Error("Successfully created the ticket but failed to get its detail");
        return Result.Success(ticketDetail);
    }
}