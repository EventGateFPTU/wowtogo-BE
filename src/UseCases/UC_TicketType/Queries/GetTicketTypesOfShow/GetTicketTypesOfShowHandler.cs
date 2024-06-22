using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_TicketType;
using MediatR;

namespace UseCases.UC_TicketType.Queries.GetTicketTypesOfShow;
public class GetTicketTypesOfShowHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetTicketTypesOfShowQuery, Result<GetTicketTypesOfShowResponse>>
{
    public async Task<Result<GetTicketTypesOfShowResponse>> Handle(GetTicketTypesOfShowQuery request, CancellationToken cancellationToken)
    {
        Show? show = await unitOfWork.ShowRepository.FindAsync(s => s.Id.Equals(request.ShowId), cancellationToken: cancellationToken);
        if (show is null) return Result.NotFound("Show is not found");
        IEnumerable<GetTicketTypeDetailsResponse> ticketTypes = await unitOfWork.TicketTypeRepository
            .GetTicketTypesOfShowAsync(request.ShowId, request.PageSize, request.PageNumber, cancellationToken: cancellationToken);
        return Result.Success(
            new GetTicketTypesOfShowResponse(
                TicketTypes: ticketTypes,
                PageSize: request.PageSize,
                PageNumber: request.PageNumber
            )
        );
    }
}