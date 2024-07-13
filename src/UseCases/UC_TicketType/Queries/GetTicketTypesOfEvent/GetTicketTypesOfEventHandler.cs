using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Responses.Responses_TicketType;
using Domain.Responses.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UseCases.Mapper.Mapper_TicketType;

namespace UseCases.UC_TicketType.Queries.GetTicketTypesOfEvent;

public class GetTicketTypesOfEventHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetTicketTypesOfEventQuery, Result<PaginatedResponse<GetTicketTypesAndShowsOfEventResponse>>>
{
    public async Task<Result<PaginatedResponse<GetTicketTypesAndShowsOfEventResponse>>> Handle(GetTicketTypesOfEventQuery request, CancellationToken cancellationToken)
    {
        var targetEvent = await unitOfWork.EventRepository.FindAsync(e => e.Id.Equals(request.EventId), cancellationToken: cancellationToken);
        if (targetEvent is null) return Result.NotFound("eventId not found");

        var ticketTypesAndShows = unitOfWork.TicketTypeShowRepository.DBSet().AsNoTracking();
        ticketTypesAndShows = ticketTypesAndShows
            .Include(t => t.TicketType)
            .Include(t => t.Show)
            .Where(t => t.Show.EventId.Equals(request.EventId));

        var count = ticketTypesAndShows.Count();
        var ticketTypesAndShowsOfEvent = await ticketTypesAndShows
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(t => t.MapToGetTicketTypesAndShowsOfEventResponse())
            .ToListAsync(cancellationToken);
        var result = new PaginatedResponse<GetTicketTypesAndShowsOfEventResponse>(
            Data:  ticketTypesAndShowsOfEvent,
            PageNumber: request.PageNumber,
            PageSize: request.PageSize,
            Count: count
        );
        return Result.Success(result, "Get Ticket Types with Shows successfully");
    }
}