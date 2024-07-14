using Domain.Models;
using Domain.Responses.Responses_User;

namespace UseCases.Mapper.Mapper_User;

public static class PublicUserDetailResponseMapper
{
    public static PublicUserDetailResponse MapToPublicUserDetailResponse(
        this User user)
        => new (
            Id: user.Id,
            Email: user.Email,
            Name: $"{user.FirstName} {user.LastName}"
        );
}