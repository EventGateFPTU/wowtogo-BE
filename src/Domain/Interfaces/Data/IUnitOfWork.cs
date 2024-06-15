using Domain.Interfaces.Data.IRepositories;

namespace Domain.Interfaces.Data;
public interface IUnitOfWork : IDisposable
{
    IArticleRepository Articles { get; }
    IEventRepository Events { get; }
    IOrderRepository Orders { get; }
    IUserRepository Users { get; }
    ITicketRepository Tickets { get; }
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
}