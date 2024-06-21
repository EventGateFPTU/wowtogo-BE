using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_Ticket;
using MediatR;

namespace UseCases.UC_Ticket.Queries.GetTicketDetail;
public record GetTicketDetailHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetTicketDetailQuery, Result<GetTicketDetailResponse>>
{
    public async Task<Result<GetTicketDetailResponse>> Handle(GetTicketDetailQuery request, CancellationToken cancellationToken)
    {
        GetTicketDetailResponse? ticket = await unitOfWork.TicketRepository.GetTicketDetail(request.TicketId, cancellationToken: cancellationToken);
        if (ticket is null) return Result.NotFound("Ticket is not found");
        return Result.Success(ticket, "Ticket is found successfully");
    }
}
