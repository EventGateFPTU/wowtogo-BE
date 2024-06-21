using Domain.Interfaces.Data.IRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;
public class EventRepository(WowToGoDBContext context) : RepositoryBase<Event>(context), IEventRepository
{
}