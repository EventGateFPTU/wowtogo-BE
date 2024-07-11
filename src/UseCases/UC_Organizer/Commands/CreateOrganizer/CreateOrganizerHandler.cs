using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_Organizer;
using MediatR;
using UseCases.Common.Models;

namespace UseCases.UC_Organizer.Commands.CreateOrganizer
{
	public class CreateOrganizerHandler(IUnitOfWork unitOfWork, CurrentUser currentUser) : IRequestHandler<CreateOrganizerCommand, Result<CreateOrganizerResponse>>
	{
		public async Task<Result<CreateOrganizerResponse>> Handle(CreateOrganizerCommand request, CancellationToken cancellationToken)
		{
			//checked if user is already a organizer
			Organizer? organizer = await unitOfWork.OrganizerRepository.FindAsync(o => o.Id == currentUser.User.Id, cancellationToken: cancellationToken);
			if (organizer is not null)
			{
				return Result.NotFound("User is already an organizer");
			}
			//create organizer
			if (organizer is null)
			{
				organizer = new()
				{
					Id = currentUser.User.Id,
					OrganizationName = request.OrganizationName,
					Description = request.Description
				};
				unitOfWork.OrganizerRepository.Add(organizer);
			}
			if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to create organizer");
			return Result.Success(new CreateOrganizerResponse(organizer.Id, organizer.OrganizationName, organizer.Description), "Organizer created successfully");

		}
	}
}
