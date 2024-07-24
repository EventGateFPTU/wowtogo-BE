using Ardalis.Result;
using MediatR;

namespace UseCases.UC_Staff.Queries.GetCheckinInfo;

public record GetCheckinInfoQuery(string Code, Guid ShowId) : IRequest<Result<GetCheckinInfoResponse>>;