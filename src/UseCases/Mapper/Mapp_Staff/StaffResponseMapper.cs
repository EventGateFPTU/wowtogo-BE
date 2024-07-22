using Domain.Models;
using Domain.Responses.Responses_Staff;

namespace UseCases.Mapper.Mapp_Staff;

public static class StaffResponseMapper
{
    public static StaffResponse MapToStaffResponse(this Staff staff)
    {
        return new StaffResponse(
            Id: staff.Id,
            UserId: staff.UserId,
            Fullname: staff.User.FirstName +" "+ staff.User.LastName,
            Email: staff.User.Email
            );
    }
}