using Ardalis.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.UC_Event.Commands.DeleteEvent
{
    public record DeleteEventCommand(Guid EventId) : IRequest<Result>;
}
