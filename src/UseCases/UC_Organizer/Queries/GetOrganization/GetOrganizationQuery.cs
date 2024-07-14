using Ardalis.Result;
using Domain.Responses.Responses_Organizer;
using MediatR;

namespace UseCases.UC_Organizer.Queries.GetOrganization;

public record GetOrganizationQuery : IRequest<Result<OrganizerDB>>;