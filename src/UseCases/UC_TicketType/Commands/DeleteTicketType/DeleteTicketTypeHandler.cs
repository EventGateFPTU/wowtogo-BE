using Ardalis.Result;
using Domain.Enums;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;

namespace UseCases.UC_TicketType.Commands.DeleteTicketType;
public class DeleteTicketTypeHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteTicketTypeCommand, Result>
{
    public async Task<Result> Handle(DeleteTicketTypeCommand request, CancellationToken cancellationToken)
    {
        TicketType? checkingTicketType = await unitOfWork.TicketTypeRepository.GetTicketIncludingEventAsync(request.TicketTypeId, cancellationToken);
        if (checkingTicketType is null) return Result.NotFound("Ticket Type not found");
        if (checkingTicketType.Show?.Event is null) return Result.Error("This Ticket Type is not associated with any Event");
        if (checkingTicketType.Show?.Event?.Status == EventStatusEnum.Published) return Result.Error("Its event has been published, you can't delete it");
        if (checkingTicketType.Show?.Event?.Status == EventStatusEnum.Canceled) return Result.Error("Its event has been canceled, you can't delete it");
        if (checkingTicketType.Show?.Event?.Status == EventStatusEnum.Completed) return Result.Error("Its event has been completed, you can't delete it");
        unitOfWork.TicketTypeRepository.Remove(checkingTicketType);
        if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to delete Ticket Type");
        return Result.SuccessWithMessage("Ticket Type is deleted successfully");
    }
}