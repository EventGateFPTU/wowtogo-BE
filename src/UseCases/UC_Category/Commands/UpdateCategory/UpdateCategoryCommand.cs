using Ardalis.Result;
using MediatR;

namespace UseCases.UC_Category.Commands.UpdateCategory
{
	public record class UpdateCategoryCommand(
						Guid CategoryId,
										string name
					) : IRequest<Result>;
}
