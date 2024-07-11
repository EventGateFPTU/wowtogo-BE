using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Responses.Responses_Organizer;
using MediatR;

namespace UseCases.UC_Organizer.Queries.GetAllOrganizer
{
	public class GetAllOrganizerHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllOrganizerQuery, Result<GetAllOrganizerResponse>>
	{
		public async Task<Result<GetAllOrganizerResponse>> Handle(GetAllOrganizerQuery request, CancellationToken cancellationToken)
		{
			IEnumerable<OrganizerDB> organizerDBs = await unitOfWork.OrganizerRepository.GetAllOrganizerAsync(request.PageNumber, request.PageSize, request.SearchTerm, cancellationToken);
			if (!organizerDBs.Any()) return Result.NotFound("Organizers are not found");
			GetAllOrganizerResponse result = new(request.PageNumber, request.PageSize, organizerDBs);
			return Result.Success(result, "Organizers are found successfully");
		}
	}
}

