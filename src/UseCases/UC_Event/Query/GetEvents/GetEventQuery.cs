using Ardalis.Result;
using Domain.Responses.Responses_Event;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.UC_Event.Query.GetEvents
{
    public record GetEventQuery(Guid EventID) :IRequest<Result<GetEventResponse>>;
   
}
