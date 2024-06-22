using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;

namespace UseCases.UC_TicketType.Commands.DeleteTicketType;
public class DeleteTicketTypeHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteTicketTypeCommand, Result>
{
    public async Task<Result> Handle(DeleteTicketTypeCommand request, CancellationToken cancellationToken)
    {
        TicketType? checkingTicketType = await unitOfWork.TicketTypeRepository.FindAsync(tt => tt.Id == request.TicketTypeId,
                                                                                        cancellationToken: cancellationToken);
        if (checkingTicketType is null) return Result.NotFound("Ticket Type not found");
        IEnumerable<Order> orders = await unitOfWork.OrderRepository.FindManyAsync(o => o.TicketTypeId.Equals(request.TicketTypeId),
                                                                                 cancellationToken: cancellationToken);
        if (orders.Any()) return Result.Error("This ticket type can not be deleted because it has been ordered.");
        unitOfWork.TicketTypeRepository.Remove(checkingTicketType);
        if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to delete Ticket Type");
        return Result.SuccessWithMessage("Ticket Type is deleted successfully");
    }
}