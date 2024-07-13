using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Responses.Responses_Event;
using Domain.Responses.Shared;
using MediatR;

namespace UseCases.UC_Event.Query.GetAllEvents
{
    public class GetAllEventsHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllEventsQuery, Result<PaginatedResponse<EventDB>>>
    {
        public async Task<Result<PaginatedResponse<EventDB>>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            PaginatedResponse<EventDB> eventDBs = await unitOfWork.EventRepository.GetAllEventAsync(request.PageNumber, request.PageSize, request.SearchTerm, false, cancellationToken);
            return Result.Success(eventDBs, "Events are found successfully");
        }
    }
}
