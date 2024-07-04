using Domain.Interfaces.Data.IRepositories;
using Domain.Models;

namespace Infrastructure.Data.Repositories;
public class TicketTypeShowRepository(WowToGoDBContext context) : RepositoryBase<TicketTypeShow>(context), ITicketTypeShowRepository;