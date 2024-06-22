namespace Domain.Responses.Responses_Staff;

public record GetEventStaffsResponse(IEnumerable<StaffResponse> Staffs,
    int PageNumber,
    int PageSize);