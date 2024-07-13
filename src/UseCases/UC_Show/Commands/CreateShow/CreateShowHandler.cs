using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_Show;
using MediatR;
using UseCases.Mapper.Mapper_Show;

namespace UseCases.UC_Show.Commands.CreateShow;
public class CreateShowHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateShowCommand, Result<CreateShowResponse>>
{
    public async Task<Result<CreateShowResponse>> Handle(CreateShowCommand request, CancellationToken cancellationToken)
    {
        Event? checkingEvent = await unitOfWork.EventRepository.FindAsync(e => e.Id.Equals(request.EventId));
        if (checkingEvent is null) return Result.NotFound("Event is not found");
        if (request.StartsAt >= request.EndsAt) return Result.Error("Starts at should be less than ends at");
        Show show = new()
        {
            EventId = request.EventId,
            Title = request.Title,
            StartsAt = request.StartsAt,
            EndsAt = request.EndsAt
        };
        unitOfWork.ShowRepository.Add(show);
        if (!await unitOfWork.SaveChangesAsync()) return Result.Error("Failed to create show");
        return Result.Success(show.MapToCreateShowResponse(), "Show is created successfully");
    }
}