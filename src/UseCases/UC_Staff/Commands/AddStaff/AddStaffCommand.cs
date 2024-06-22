using Ardalis.Result;
using MediatR;

namespace UseCases.UC_Staff.Commands.AddStaff;
public record AddStaffCommand(Guid UserId,Guid EventId) : IRequest<Result>;