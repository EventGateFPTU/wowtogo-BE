using Domain.Interfaces.Data.IRepositories;
using Domain.Models;

namespace Infrastructure.Data.Repositories;
public class OrganizerRepository(WowToGoDBContext dbContext) : RepositoryBase<Organizer>(dbContext), IOrganizerRepository
{
}