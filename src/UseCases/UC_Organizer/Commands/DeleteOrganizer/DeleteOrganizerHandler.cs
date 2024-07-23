using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;
using UseCases.Common.Models;

namespace UseCases.UC_Organizer.Commands.DeleteOrganizer
{
	public class DeleteOrganizerHandler(IUnitOfWork unitOfWork, CurrentUser currentUser) : IRequestHandler<DeleteOrganizerCommand, Result>
	{
		public async Task<Result> Handle(DeleteOrganizerCommand request, CancellationToken cancellationToken)
		{
            //role check
            //check if the user is authorized to update the organizer

            Organizer? checkingOrganizer = await unitOfWork.OrganizerRepository.FindAsync(c => c.Id.Equals(request.Id), cancellationToken: cancellationToken);
			if (checkingOrganizer is null) return Result.NotFound("Organizer not found");
            if (currentUser.User.Id != checkingOrganizer.Id) return Result.Unavailable("You are not authorized to delete this organizer");
            unitOfWork.OrganizerRepository.Remove(checkingOrganizer);
			if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to delete organizer");
			return Result.SuccessWithMessage("Organizer is deleted successfully");
		}
	}
}
