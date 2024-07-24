using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;
using UseCases.Common.Models;

namespace UseCases.UC_TicketType.Commands.UpdateTicketType;
public class UpdateTicketTypeHandler(IUnitOfWork unitOfWork, CurrentUser currentUser) : IRequestHandler<UpdateTicketTypeCommand, Result>
{
    public async Task<Result> Handle(UpdateTicketTypeCommand request, CancellationToken cancellationToken)
    {
        TicketType? checkingTicketType = await unitOfWork.TicketTypeRepository.GetTicketIncludingEventAsync(request.Id, cancellationToken: cancellationToken);
        if (checkingTicketType is null) return Result.NotFound("Ticket type not found");
        Event? checkingEvent = await unitOfWork.EventRepository.GetEventWithOrganizer(checkingTicketType.EventId, cancellationToken: cancellationToken);
        if (checkingEvent is null) return Result.NotFound("Ticket type's event is not found");
        if (!IsCurrentUserOwnEvent(checkingEvent)) return Result.Forbidden();
        if (!IsEventDraftModifiable(checkingTicketType)) return Result.Error($"Its event status is {checkingTicketType.Event.Status}, can not modified");
        var (validResult, message) = checkingTicketType.IsValid();
        if (!validResult) return Result.Error(message);
        {
            checkingTicketType.Name = request.Name;
            checkingTicketType.Description = request.Description;
            checkingTicketType.ImageUrl = request.ImageUrl;
            checkingTicketType.Price = request.Price;
            checkingTicketType.FromDate = request.FromDate;
            checkingTicketType.ToDate = request.ToDate;
            checkingTicketType.Amount = request.Amount;
            checkingTicketType.LeastAmountBuy = request.LeastAmountBuy;
            checkingTicketType.MostAmountBuy = request.MostAmountBuy;
            checkingTicketType.UpdatedAt = DateTimeOffset.UtcNow;
        }
        if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to update ticket type");
        return Result.SuccessWithMessage("Ticket type is updated successfully");
    }
    private bool IsCurrentUserOwnEvent(Event checkingEvent)
        => checkingEvent.Organizer.Id.Equals(currentUser.User!.Id);
    private bool IsEventDraftModifiable(TicketType checkingTicketType)
        => checkingTicketType.Event.Status == Domain.Enums.EventStatusEnum.Draft;
}