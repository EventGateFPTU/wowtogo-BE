using Ardalis.Result;
using Domain.Enums;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;
using UseCases.Common.Models;

namespace UseCases.UC_TicketType.Commands.DeleteTicketType;
public class DeleteTicketTypeHandler(IUnitOfWork unitOfWork, CurrentUser currentUser) : IRequestHandler<DeleteTicketTypeCommand, Result>
{
    public async Task<Result> Handle(DeleteTicketTypeCommand request, CancellationToken cancellationToken)
    {
        TicketType? checkingTicketType = await unitOfWork.TicketTypeRepository.GetTicketIncludingEventAsync(request.TicketTypeId, cancellationToken);
        if (checkingTicketType is null) return Result.NotFound("Ticket Type not found");
        if (!IsCurrentUserOrganizer(checkingTicketType.Event.Organizer)) return Result.Forbidden();
        if (checkingTicketType.Event is null) return Result.Error("This Ticket Type is not associated with any Event");
        if (checkingTicketType.Event?.Status == EventStatusEnum.Published) return Result.Error("Its event has been published, you can't delete it");
        if (checkingTicketType.Event?.Status == EventStatusEnum.Canceled) return Result.Error("Its event has been canceled, you can't delete it");
        if (checkingTicketType.Event?.Status == EventStatusEnum.Completed) return Result.Error("Its event has been completed, you can't delete it");
        unitOfWork.TicketTypeRepository.Remove(checkingTicketType);
        if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to delete Ticket Type");
        return Result.SuccessWithMessage("Ticket Type is deleted successfully");
    }
    private bool IsCurrentUserOrganizer(Organizer organizer)
        => organizer.Id.Equals(currentUser.User!.Id);
}