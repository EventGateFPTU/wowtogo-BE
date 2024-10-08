﻿using Domain.Models;
using Domain.Responses.Responses_Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UseCases.Mapper.Mapper_Category;

namespace UseCases.Mapper.Mapper_Event
{
    public static class GetEventResponseMapper
    {
        public static GetEventResponse MapToGetEventResponse(this Event mappingEvent)
        {
            var minPrice = mappingEvent.TicketTypes.Count > 0 ? mappingEvent.TicketTypes.Min(t => t.Price) : 0;
            return new GetEventResponse(
                Id: mappingEvent.Id,
                Title: mappingEvent.Title,
                Description: mappingEvent.Description,
                Location: mappingEvent.Location,
                Status: mappingEvent.Status.ToString(),
                OrganizerName: mappingEvent.Organizer?.OrganizationName ?? string.Empty,
                OrganizerImageUrl: mappingEvent.Organizer?.ImageUrl ?? string.Empty,
                OrganizerDescription: mappingEvent.Organizer?.Description ?? string.Empty,
                BackgroundImageUrl: mappingEvent.BackgroundImageUrl,
                BannerImageUrl: mappingEvent.BannerImageUrl,
                Categories: mappingEvent.EventCategories.Select( ec => ec.Category.MapCategoryDB()).ToArray(),
                // MaxTickets: mappingEvent.MaxTickets,
                AdditionalImages: mappingEvent.AdditionalImages.Select(x => x.Url).ToArray(),
                FromPrice: minPrice,
                CreatedAt: mappingEvent.CreatedAt
                );
        }
    }
}

