using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;

namespace UseCases.UC_Category.Commands.UpdateCategory
{
	public class UpdateCategoryHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateCategoryCommand, Result>
	{
		public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
		{
			Category? checkingCategory = await unitOfWork.CategoryRepository.FindAsync(c => c.Id.Equals(request.CategoryId), cancellationToken: cancellationToken);
			if (checkingCategory is null) return Result.NotFound("Category not found");
			checkingCategory.Name = request.name;
			if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to update category");
			return Result.SuccessWithMessage("Category is updated successfully");

		}
	}
}
