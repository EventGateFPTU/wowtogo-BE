using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;
using UseCases.Common.Models;

namespace UseCases.UC_Organizer.Commands.UpdateOrganizer
{
	public class UpdateOrganizerHandler(IUnitOfWork unitOfWork, CurrentUser currentUser) : IRequestHandler<UpdateOrganizerCommand, Result<Organizer>>
	{
		public async Task<Result<Organizer>> Handle(UpdateOrganizerCommand request, CancellationToken cancellationToken)
		{
			
			/*var checkingOrganizer = await unitOfWork.OrganizerRepository.FindAsync(c => c.Id.Equals(request.Id), trackChanges: true, cancellationToken: cancellationToken);
			if (checkingOrganizer is null) return Result.NotFound("Organizer not found");
			checkingOrganizer.OrganizationName = request.OrganizationName;
			checkingOrganizer.Description = request.Description;
			if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to update organizer");
			return Result.SuccessWithMessage("Organizer is updated successfully");*/

            //check if the user is authorized to update the organizer
			Organizer? organizer = await unitOfWork.OrganizerRepository.FindAsync(c => c.Id.Equals(request.Id), trackChanges: true, cancellationToken: cancellationToken);
			if (organizer is null) return Result.NotFound("Organizer not found");
			if(currentUser.User.Id != organizer.Id ) return Result.Unavailable("You are not authorized to update this organizer");
            organizer.OrganizationName = request.OrganizationName;
			organizer.Description = request.Description;

			
			if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to update organizer");
			return Result.Success(organizer,"Organizer is updated successfully");



        }
    }
}
