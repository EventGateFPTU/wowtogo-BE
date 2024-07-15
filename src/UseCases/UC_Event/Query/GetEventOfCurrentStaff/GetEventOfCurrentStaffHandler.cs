using Ardalis.Result;
using Domain.Interfaces.Data;
using Domain.Responses.Responses_Event;
using Domain.Responses.Shared;
using MediatR;
using UseCases.Common.Models;

namespace UseCases.UC_Event.Query.GetEventOfCurrentStaff;
public class GetEventOfCurrentStaffHandler(IUnitOfWork unitOfWork, CurrentUser currentUser) : IRequestHandler<GetEventOfCurrentStaffQuery, Result<PaginatedResponse<GetEventResponse>>>
{
    public async Task<Result<PaginatedResponse<GetEventResponse>>> Handle(GetEventOfCurrentStaffQuery request, CancellationToken cancellationToken)
    {
        if (currentUser.User?.Id is null) return Result.Error("User Id is null");
        Guid currentUserId = currentUser.User.Id;
        PaginatedResponse<GetEventResponse> result = await unitOfWork.EventRepository.GetEventsOfStaff(staffId: currentUserId,
                                                            pageNumber: request.PageNumber,
                                                            pageSize: request.PageSize,
                                                            cancellationToken: cancellationToken);
        return Result.Success(result, "Get event of current staff successfully");
    }
}