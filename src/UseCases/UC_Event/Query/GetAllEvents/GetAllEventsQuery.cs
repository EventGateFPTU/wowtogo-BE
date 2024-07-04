using Ardalis.Result;
using Domain.Responses.Responses_Event;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.UC_Event.Query.GetAllEvents
{
    public record GetAllEventsQuery(int PageNumber = 1, int PageSize = 10) :IRequest<Result<GetAllEventsResponse>>;
    
}
