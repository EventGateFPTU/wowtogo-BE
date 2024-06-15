using Domain.Interfaces.Data;
using Domain.Interfaces.Data.IRepositories;
using Infrastructure.Data.Repositories;

namespace Infrastructure.Data;
public class UnitOfWork(WowToGoDBContext context) : IUnitOfWork
{
    private readonly WowToGoDBContext _context = context;
    private IArticleRepository _articleRepository = null!;
    private IEventRepository _eventRepository = null!;
    private IOrderRepository _orderRepository = null!;
    private IUserRepository _userRepository = null!;
    private ITicketRepository _ticketRepository = null!;
    private bool _disposed = true;

    public IArticleRepository Articles => _articleRepository ??= new ArticleRepository(_context);

    public IEventRepository Events => _eventRepository ??= new EventRepository(_context);


    public IOrderRepository Orders => _orderRepository ??= new OrderRepository(_context);

    public IUserRepository Users => _userRepository ??= new UserRepository(_context);

    public ITicketRepository Tickets => _ticketRepository ??= new TicketRepository(_context);

    public void Dispose()
    {
        if (_disposed) _disposed = !Dispose(true).Result;
    }

    public async Task<bool> Dispose(bool disposing)
    {
        if (!disposing) return false;
        await _context.DisposeAsync();
        GC.SuppressFinalize(this);
        return true;
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken) > 0;
}