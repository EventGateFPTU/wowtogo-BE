using Domain.Interfaces.Data.IRepositories;
using Domain.Models;

namespace Infrastructure.Data.Repositories;
public class AttendeeRepository(WowToGoDBContext dbContext) : RepositoryBase<Attendee>(dbContext), IAttendeeRepository
{
}