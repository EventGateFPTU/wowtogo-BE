using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_Category;
using MediatR;
using UseCases.Common.Models;
using UseCases.Mapper.Mapper_Category;

namespace UseCases.UC_Category.Commands.CreateCategory
{
	public class CreateCategoryHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCategoryCommand, Result<CategoryDB>>
	{
		public async Task<Result<CategoryDB>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
		{
			var existedName =
				await unitOfWork.CategoryRepository.FindAsync(c => c.Name.ToUpper().Equals(request.Name.ToUpper()), cancellationToken: cancellationToken);
			if (existedName is not null) return Result.Error("Category is duplicated");
			Category newCategory = new()
			{
				Name = request.Name,
			};
			unitOfWork.CategoryRepository.Add(newCategory);
			if (!await unitOfWork.SaveChangesAsync(cancellationToken)) return Result.Error("Failed to create category");
			var createdCategory = await unitOfWork.CategoryRepository.FindAsync(c => c.Name.ToUpper().Equals(request.Name.ToUpper()), cancellationToken: cancellationToken);
			return createdCategory is not null? Result.Success(createdCategory.MapCategoryDB(), "Category created successfully") : Result.Error("Failed to create category");;
		}
	}
}
