using Ardalis.Result;
using Domain.Responses.Responses_Staff;
using MediatR;

namespace UseCases.UC_Staff.Queries.GetShowStaffs;

public record GetShowStaffsQuery(Guid ShowId) : IRequest<Result<List<StaffResponse>>>;
