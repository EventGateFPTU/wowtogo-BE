using Domain.Interfaces.Data.IRepositories;
using Domain.Models;

namespace Infrastructure.Data.Repositories;
public class ShowRepository(WowToGoDBContext dbContext) : RepositoryBase<Show>(dbContext), IShowRepository
{

}