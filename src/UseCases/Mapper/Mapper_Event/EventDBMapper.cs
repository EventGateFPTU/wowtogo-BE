using Domain.Models;
using Domain.Responses.Responses_Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Mapper.Mapper_Event
{
    public static class EventDBMapper
    {
        public static EventDB MapEventDB(this Event _event)
            => new EventDB(EventID: _event.Id,
                Title: _event.Title,
                Description: _event.Description,
                Location: _event.Location,
                Status: _event.Status.ToString(),
                OrganizerName: _event.Organizer?.OrganizationName ?? string.Empty,
                MaxTickets: _event.MaxTickets,
                CreatedAt: _event.CreatedAt

                );
    }
}
