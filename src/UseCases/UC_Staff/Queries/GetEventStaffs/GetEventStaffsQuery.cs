using Ardalis.Result;
using Domain.Responses.Responses_Staff;
using MediatR;

namespace UseCases.UC_Staff.Queries.GetEventStaffs;

public record GetEventStaffsQuery(Guid EventId, int PageNumber = 1, int PageSize = 10) : IRequest<Result<GetEventStaffsResponse>>;