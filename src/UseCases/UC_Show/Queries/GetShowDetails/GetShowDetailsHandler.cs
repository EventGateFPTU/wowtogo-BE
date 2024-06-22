using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_Show;
using MediatR;

namespace UseCases.UC_Show.Queries.GetShowDetails;
public class GetShowDetailsHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetShowDetailsQuery, Result<GetShowDetailResponse>>
{
    public async Task<Result<GetShowDetailResponse>> Handle(GetShowDetailsQuery request, CancellationToken cancellationToken = default)
    {
        GetShowDetailResponse? checkingShow = await unitOfWork.ShowRepository.GetShowDetailAsync(request.ShowId, cancellationToken: cancellationToken);
        if (checkingShow is null) return Result.NotFound("Show is not found");
        return Result.Success(checkingShow, "Show is found successfully");
    }
}