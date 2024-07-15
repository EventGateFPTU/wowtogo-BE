using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Responses.Responses_Checkin;
using Domain.Responses.Responses_Ticket;
using Domain.Responses.Shared;
using MediatR;

namespace UseCases.UC_Ticket.Query.GetCheckinsByEvent;
public class GetCheckinsByEventHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCheckinsByEventQuery, Result<PaginatedResponse<GetCheckinDetailResponse>>>
{
    public async Task<Result<PaginatedResponse<GetCheckinDetailResponse>>> Handle(GetCheckinsByEventQuery request, CancellationToken cancellationToken)
    {
        PaginatedResponse<GetCheckinDetailResponse> gettingTickets = await unitOfWork.CheckinRepository.GetCheckinsByEventAsync(eventId: request.EventId,
                                                                                                                                            pageNumber: request.PageNumber,
                                                                                                                                            pageSize: request.PageSize,
                                                                                                                                            cancellationToken: cancellationToken);
        return Result.Success(gettingTickets, "Get Checkin Successfully!");
    }
}