using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Models;
using Domain.Responses.Responses_Staff;
using Domain.Responses.Shared;
using MediatR;
using UseCases.Common.Models;

namespace UseCases.UC_Staff.Queries.GetEventStaffs;

public class GetEventStaffHandler(IUnitOfWork unitOfWork, CurrentUser currentUser) : IRequestHandler<GetEventStaffsQuery, Result<PaginatedResponse<StaffResponse>>>
{
    public async Task<Result<PaginatedResponse<StaffResponse>>> Handle(GetEventStaffsQuery request, CancellationToken cancellationToken)
    {
        var targetEvent = await unitOfWork.EventRepository.GetEventWithOrganizer(request.EventId, cancellationToken: cancellationToken);
        if (targetEvent is null) return Result.NotFound("eventId not found");
        if (!CanCurrentUserSeeStaff(targetEvent)) return Result.Forbidden();
        // TODO: check if current user can see event's staffs
        // note: can't add self
        PaginatedResponse<StaffResponse> staffs = await unitOfWork.StaffRepository.GetEventStaffsAsync(
            eventId: request.EventId,
            pageNumber: request.PageNumber,
            pageSize: request.PageSize,
            cancellationToken: cancellationToken
        );
        return Result.Success(staffs, "Get staffs successfully");
    }
    private bool CanCurrentUserSeeStaff(Event checkingEvent)
        => checkingEvent.Organizer.Id.Equals(currentUser.User!.Id) || checkingEvent.Staffs.Any(s => s.UserId.Equals(currentUser.User!.Id));
}