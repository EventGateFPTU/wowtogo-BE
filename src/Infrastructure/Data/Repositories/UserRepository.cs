using Domain.Interfaces.Data.IRepositories;
using Domain.Models;

namespace Infrastructure.Data.Repositories;
public class UserRepository(WowToGoDBContext context) : RepositoryBase<User>(context), IUserRepository
{
    
}