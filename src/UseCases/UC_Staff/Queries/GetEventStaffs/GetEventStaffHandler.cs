using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_Staff;
using MediatR;

namespace UseCases.UC_Staff.Queries.GetEventStaffs;

public class GetEventStaffHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetEventStaffsQuery, Result<GetEventStaffsResponse>>
{
    public async Task<Result<GetEventStaffsResponse>> Handle(GetEventStaffsQuery request, CancellationToken cancellationToken)
    {
        var targetEvent = await unitOfWork.EventRepository.FindAsync(e => e.Id.Equals(request.EventId), cancellationToken: cancellationToken);
        if (targetEvent is null) return Result.NotFound("eventId not found");
        // TODO: check if current user can see event's staffs
        // note: can't add self
        var staffs = await unitOfWork.StaffRepository.GetEventStaffsAsync(
            eventId: request.EventId,
            pageNumber: request.PageNumber,
            pageSize: request.PageSize,
            cancellationToken: cancellationToken
        );
        var response = new GetEventStaffsResponse(staffs, request.PageNumber, request.PageSize);
        return Result.Success(response, "Get staffs successfully");
    }
}