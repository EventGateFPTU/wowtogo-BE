using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Responses.Responses_Organizer;
using Domain.Responses.Shared;
using MediatR;

namespace UseCases.UC_Organizer.Queries.GetAllOrganizer
{
    public class GetAllOrganizerHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllOrganizerQuery, Result<PaginatedResponse<OrganizerDB>>>
    {
        public async Task<Result<PaginatedResponse<OrganizerDB>>> Handle(GetAllOrganizerQuery request, CancellationToken cancellationToken)
        {
            PaginatedResponse<OrganizerDB> result = await unitOfWork.OrganizerRepository.GetAllOrganizerAsync(request.PageNumber, request.PageSize, request.SearchTerm, false, cancellationToken);
            return Result.Success(result, "Get organizer successfully");
        }
    }
}

