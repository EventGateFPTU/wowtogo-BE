using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using MediatR;

namespace UseCases.UC_Category.Commands.CreateCategory
{
	public class CreateCategoryHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCategoryCommand, Result>
	{
		public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
		{
			Category newCategory = new()
			{
				Id = Guid.NewGuid(),
				Name = request.name,
			};
			unitOfWork.CategoryRepository.Add(newCategory);
			if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to create category");
			return Result.SuccessWithMessage("Category created successfully");
		}
	}
}
