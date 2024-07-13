using Ardalis.Result;
using Domain.Events.Tickets;
using Domain.Interfaces.Data;
using Domain.Interfaces.Email;
using Domain.Models;
using Domain.Responses.Responses_Ticket;
using MediatR;
using UseCases.Common.Models;
using UseCases.Utils;

namespace UseCases.UC_Ticket.Commands.CreateTicket;
public class CreateTicketHandler(IUnitOfWork unitOfWork, IMailService mailService) : IRequestHandler<CreateTicketCommand, Result<CreateTicketResponse>>
{
    public async Task<Result<CreateTicketResponse>> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        Attendee? checkingAttendee = await unitOfWork.AttendeeRepository.FindAsync(a => a.Id.Equals(request.AttendeeId), cancellationToken: cancellationToken);
        if (checkingAttendee is null) return Result.NotFound("Attendee is not found !");
        TicketType? checkingTicketType = await unitOfWork.TicketTypeRepository.FindAsync(tt => tt.Id.Equals(request.TicketTypeId), cancellationToken: cancellationToken);
        if (checkingTicketType is null) return Result.NotFound("Ticket Type is not found !");
        var code = GenerateRandom6CharsCode.Generate();
        // TODO: What if duplicated?

        Ticket ticket = new()
        {
            TicketTypeId = request.TicketTypeId,
            AttendeeId = request.AttendeeId,
            Code = code,  
        };
        unitOfWork.TicketRepository.Add(ticket);
        // OpenFga
        ticket.AddDomainEvent(new TicketCreatedEvent(TicketId: ticket.Id, TicketTypeId: request.TicketTypeId));
        if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to create ticket");
        CreateTicketResponse? ticketDetail = await unitOfWork.TicketRepository.GetCreatedTicketDetail(ticket.Id, cancellationToken: cancellationToken);
        if (ticketDetail == null) return Result.Error("Successfully created the ticket but failed to get its detail");
        if (!await mailService.SendTicketGeneratedMailAsync(userName: checkingAttendee.Email, code: ticket.Code)) return Result.Error("Error sending mail");
        return Result.Success(ticketDetail);
    }
}