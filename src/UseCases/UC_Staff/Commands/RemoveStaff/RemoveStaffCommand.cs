using Ardalis.Result;
using MediatR;

namespace UseCases.UC_Staff.Commands.RemoveStaff;
public record RemoveStaffCommand(Guid StaffId) : IRequest<Result>;