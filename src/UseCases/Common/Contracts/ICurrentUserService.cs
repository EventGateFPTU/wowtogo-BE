using UseCases.Common.Models;

namespace UseCases.Common.Contracts;

public interface ICurrentUserService
{
    Task<UserInfo?> GetUser();
}