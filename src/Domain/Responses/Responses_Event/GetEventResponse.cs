﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Responses.Responses_Event
{
    public record GetEventResponse(
        Guid EventID,
        string Title,
        string Description,
        string Location,
        string Status,
        string OrganizerName,
        int MaxTickets,
        DateTimeOffset CreatedAt);
}   
