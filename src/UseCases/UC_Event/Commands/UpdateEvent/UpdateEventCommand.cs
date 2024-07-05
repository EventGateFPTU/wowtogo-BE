using Ardalis.Result;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.UC_Event.Commands.UpdateEvent
{
    public record UpdateEventCommand(Guid Id,
        string Title,
        string Description,
        string Location,
        EventStatusEnum Status,
        Guid OrganizerID,
        int MaxTickets) : IRequest<Result>;
}
