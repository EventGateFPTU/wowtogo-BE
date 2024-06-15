using Domain.Interfaces.Data.IRepositories;
using Domain.Models;

namespace Infrastructure.Data.Repositories;
public class EventCategoryRepository(WowToGoDBContext dbContext) : RepositoryBase<EventCategory>(dbContext), IEventCategoryRepository
{

}