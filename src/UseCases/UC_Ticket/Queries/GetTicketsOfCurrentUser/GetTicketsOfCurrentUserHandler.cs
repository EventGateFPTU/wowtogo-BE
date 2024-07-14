using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Responses.Responses_Ticket;
using Domain.Responses.Shared;
using MediatR;
using UseCases.Common.Models;

namespace UseCases.UC_Ticket.Queries.GetTicketsOfCurrentUser;
public class GetTicketsOfCurrentUserHandler(IUnitOfWork unitOfWork, CurrentUser currentUser) : IRequestHandler<GetTicketsOfCurrentUserQuery, Result<PaginatedResponse<GetTicketDetailsResponse>>>
{
    public async Task<Result<PaginatedResponse<GetTicketDetailsResponse>>> Handle(GetTicketsOfCurrentUserQuery request, CancellationToken cancellationToken)
    {
        PaginatedResponse<GetTicketDetailsResponse> result = await unitOfWork.TicketRepository
                                                                .GetTicketsOfUser(userId: currentUser.User!.Id,
                                                                                pageNumber: request.PageNumber,
                                                                                pageSize: request.PageSize);
        return Result.Success(result, "Get Ticket Of Current User Successfully");
    }
}