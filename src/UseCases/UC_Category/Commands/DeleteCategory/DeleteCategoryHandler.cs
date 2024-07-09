using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;

namespace UseCases.UC_Category.Commands.DeleteCategory
{
	public class DeleteCategoryHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCategoryCommand, Result>
	{
		public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
		{
			Category? checkingCategory = await unitOfWork.CategoryRepository.FindAsync(c => c.Id.Equals(request.CategoryId), cancellationToken: cancellationToken);
			if (checkingCategory is null) return Result.NotFound("Category not found");
			unitOfWork.CategoryRepository.Remove(checkingCategory);
			if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to delete category");
			return Result.SuccessWithMessage("Category is deleted successfully");
		}
	}
}
