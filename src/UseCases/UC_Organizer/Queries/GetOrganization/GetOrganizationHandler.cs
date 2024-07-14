using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Responses.Responses_Organizer;
using MediatR;
using UseCases.Common.Models;

namespace UseCases.UC_Organizer.Queries.GetOrganization;

public class GetOrganizationHandler(IUnitOfWork unitOfWork, CurrentUser currentUser) : IRequestHandler<GetOrganizationQuery, Result<OrganizerDB>>
{
    public async Task<Result<OrganizerDB>> Handle(GetOrganizationQuery request, CancellationToken cancellationToken)
    {
        var organization = await unitOfWork.OrganizerRepository.GetCurrentOrganizationAsync(currentUser.User!.Id, cancellationToken);
        if (organization is null) return Result.NotFound("Organization not found");
        return Result.Success(organization);
    }
}