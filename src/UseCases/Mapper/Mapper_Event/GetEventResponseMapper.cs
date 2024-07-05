using Domain.Models;
using Domain.Responses.Responses_Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Mapper.Mapper_Event
{
    public static class GetEventResponseMapper
    {
        public static GetEventResponse MapToGetEventResponse(this Event mappingEvent)
        {
            return new GetEventResponse(
                EventID: mappingEvent.Id,
                Title: mappingEvent.Title,
                Description: mappingEvent.Description,
                Location: mappingEvent.Location,
                Status: mappingEvent.Status.ToString(),
                OrganizerName: mappingEvent.Organizer?.OrganizationName ?? string.Empty,
                 OrganizerImageUrl: mappingEvent.Organizer?.ImageUrl ?? string.Empty,
                BackgroundImageUrl: mappingEvent.BackgroundImageUrl,
                BannerImageUrl: mappingEvent.BannerImageUrl,
                MaxTickets: mappingEvent.MaxTickets,
                CreatedAt: mappingEvent.CreatedAt
                );
        }
    }
}

