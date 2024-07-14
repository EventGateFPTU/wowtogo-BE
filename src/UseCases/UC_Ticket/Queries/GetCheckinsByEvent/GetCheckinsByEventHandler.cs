using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Responses.Responses_Ticket;
using Domain.Responses.Shared;
using MediatR;

namespace UseCases.UC_Ticket.Query.GetCheckinsByEvent;
public class GetCheckinsByEventHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCheckinsByEventQuery, Result<PaginatedResponse<GetTicketDetailsResponse>>>
{
    public async Task<Result<PaginatedResponse<GetTicketDetailsResponse>>> Handle(GetCheckinsByEventQuery request, CancellationToken cancellationToken)
    {
        PaginatedResponse<GetTicketDetailsResponse> gettingTickets = await unitOfWork.TicketRepository.GetCheckinsByEventAsync(eventId: request.EventId,
                                                                                                                                            pageNumber: request.PageNumber,
                                                                                                                                            pageSize: request.PageSize,
                                                                                                                                            cancellationToken: cancellationToken);
        return Result.Success(gettingTickets, "Get Checkin Successfully!");
    }
}