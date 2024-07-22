using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_Ticket;
using MediatR;
using UseCases.Common.Models;

namespace UseCases.UC_Ticket.Queries.GetTicketDetail;
public record GetTicketDetailHandler(IUnitOfWork unitOfWork, CurrentUser currentUser) : IRequestHandler<GetTicketDetailQuery, Result<GetTicketDetailsResponse>>
{
    public async Task<Result<GetTicketDetailsResponse>> Handle(GetTicketDetailQuery request, CancellationToken cancellationToken)
    {
        Ticket? checkingTicket = await unitOfWork.TicketRepository.GetTicketDetailById(id: request.TicketId, cancellationToken: cancellationToken);
        if (checkingTicket is null) return Result.NotFound("Ticket is not found");
        if (!checkingTicket.Attendee.UserId.Equals(currentUser.User!.Id)) return Result.Forbidden();
        GetTicketDetailsResponse? ticket = await unitOfWork.TicketRepository.GetTicketDetail(request.TicketId, cancellationToken: cancellationToken);
        if (ticket is null) return Result.NotFound("Ticket is not found");
        return Result.Success(ticket, "Ticket is found successfully");
    }
}
