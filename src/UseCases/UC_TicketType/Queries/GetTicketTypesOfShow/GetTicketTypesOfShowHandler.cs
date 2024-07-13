using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_TicketType;
using Domain.Responses.Shared;
using MediatR;

namespace UseCases.UC_TicketType.Queries.GetTicketTypesOfShow;
public class GetTicketTypesOfShowHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetTicketTypesOfShowQuery, Result<PaginatedResponse<GetTicketTypeDetailsResponse>>>
{
    public async Task<Result<PaginatedResponse<GetTicketTypeDetailsResponse>>> Handle(GetTicketTypesOfShowQuery request, CancellationToken cancellationToken)
    {
        Show? show = await unitOfWork.ShowRepository.FindAsync(s => s.Id.Equals(request.ShowId), cancellationToken: cancellationToken);
        if (show is null) return Result.NotFound("Show is not found");
        PaginatedResponse<GetTicketTypeDetailsResponse> ticketTypes = await unitOfWork.TicketTypeRepository
            .GetTicketTypesOfShowAsync(request.ShowId, request.PageSize, request.PageNumber, cancellationToken: cancellationToken);
        return ticketTypes;
    }
}