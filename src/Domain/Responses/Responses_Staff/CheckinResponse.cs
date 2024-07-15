using Domain.Responses.Responses_Attendee;

namespace Domain.Responses.Responses_Staff;

public record CheckinResponse(
    AttendeeDetailResponse AttendeeDetail,
    bool IsCheckedIn    
);