using Ardalis.Result;
using Domain.Interfaces.Data;
using MediatR;
using UseCases.Common.Models;

namespace UseCases.UC_Organizer.Commands.UpdateOrganizer
{
	public class UpdateOrganizerHandler(IUnitOfWork unitOfWork, CurrentUser currentUser) : IRequestHandler<UpdateOrganizerCommand, Result>
	{
		public async Task<Result> Handle(UpdateOrganizerCommand request, CancellationToken cancellationToken)
		{
			var checkingOrganizer = await unitOfWork.OrganizerRepository.FindAsync(c => c.Id.Equals(request.Id), trackChanges: true, cancellationToken: cancellationToken);
			if (checkingOrganizer is null) return Result.NotFound("Organizer not found");
			checkingOrganizer.OrganizationName = request.OrganizationName;
			checkingOrganizer.Description = request.Description;
			if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to update organizer");
			return Result.SuccessWithMessage("Organizer is updated successfully");
		}
	}
}
