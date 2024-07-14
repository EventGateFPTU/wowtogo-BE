using Domain.Responses.Responses_Event;
using MediatR;

namespace UseCases.UC_Event.Query.GetOrganizerEvents;

public class GetOrganizerEventsQuery : IRequest<List<EventDB>>;