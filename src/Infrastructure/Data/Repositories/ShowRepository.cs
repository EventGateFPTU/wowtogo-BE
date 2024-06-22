using Domain.Interfaces.Data.IRepositories;
using Domain.Models;
using Domain.Responses.Responses_TicketType;
using Microsoft.EntityFrameworkCore;
using UseCases.Mapper.Mapper_TicketType;

namespace Infrastructure.Data.Repositories;
public class ShowRepository(WowToGoDBContext dbContext) : RepositoryBase<Show>(dbContext), IShowRepository
{
}