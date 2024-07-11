using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;

namespace UseCases.UC_Organizer.Commands.DeleteOrganizer
{
	public class DeleteOrganizerHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteOrganizerCommand, Result>
	{
		public async Task<Result> Handle(DeleteOrganizerCommand request, CancellationToken cancellationToken)
		{
			Organizer? checkingOrganizer = await unitOfWork.OrganizerRepository.FindAsync(c => c.Id.Equals(request.Id), cancellationToken: cancellationToken);
			if (checkingOrganizer is null) return Result.NotFound("Organizer not found");
			unitOfWork.OrganizerRepository.Remove(checkingOrganizer);
			if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to delete organizer");
			return Result.SuccessWithMessage("Organizer is deleted successfully");
		}
	}
}
