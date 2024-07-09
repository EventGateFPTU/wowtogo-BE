using Domain.Interfaces.Data;
using Domain.Interfaces.Data.IRepositories;
using Infrastructure.Data.Repositories;

namespace Infrastructure.Data;
public class UnitOfWork(WowToGoDBContext context) : IUnitOfWork
{
    // NOTE : init all repositories here
    private readonly WowToGoDBContext _context = context;
    private IArticleRepository _articleRepository = null!;
    private IAttendeeRepository _attendeeRepository = null!;
    private ICategoryRepository _categoryRepository = null!;
    private IEventCategoryRepository _eventCategoryRepository = null!;
    private IEventRepository _eventRepository = null!;
    private IOrderRepository _orderRepository = null!;
    private IOrganizerRepository _organizerRepository = null!;
    private IShowRepository _showRepository = null!;
    private IStaffRepository _staffRepository = null!;
    private ITicketRepository _ticketRepository = null!;
    private ITicketTypeRepository _ticketTypeRepository = null!;
    private IUserRepository _userRepository = null!;
    private ITicketTypeShowRepository _ticketTypeShowRepository = null!;
    private bool _disposed = true;
    // NOTE : init all repositories here
    public IArticleRepository Articles => _articleRepository ??= new ArticleRepository(_context);
    public IEventRepository Events => _eventRepository ??= new EventRepository(_context);
    public IOrderRepository Orders => _orderRepository ??= new OrderRepository(_context);
    public IUserRepository Users => _userRepository ??= new UserRepository(_context);
    public ITicketRepository Tickets => _ticketRepository ??= new TicketRepository(_context);
    public IArticleRepository ArticleRepository => _articleRepository ??= new ArticleRepository(_context);
    public IAttendeeRepository AttendeeRepository => _attendeeRepository ??= new AttendeeRepository(_context);
    public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_context);
    public IEventCategoryRepository EventCategoryRepository => _eventCategoryRepository ??= new EventCategoryRepository(_context);
    public IEventRepository EventRepository => _eventRepository ??= new EventRepository(_context);
    public IOrderRepository OrderRepository => _orderRepository ??= new OrderRepository(_context);
    public IOrganizerRepository OrganizerRepository => _organizerRepository ??= new OrganizerRepository(_context);
    public IShowRepository ShowRepository => _showRepository ??= new ShowRepository(_context);
    public IStaffRepository StaffRepository => _staffRepository ??= new StaffRepository(_context);
    public ITicketRepository TicketRepository => _ticketRepository ??= new TicketRepository(_context);
    public ITicketTypeRepository TicketTypeRepository => _ticketTypeRepository ??= new TicketTypeRepository(_context);
    public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);
    public ITicketTypeShowRepository TicketTypeShowRepository => _ticketTypeShowRepository ??= new TicketTypeShowRepository(_context);

    public void Dispose()
    {
        if (_disposed) _disposed = !Dispose(true).Result;
    }

    public async Task<bool> Dispose(bool disposing)
    {
        if (!disposing) return false;
        await Task.Delay(0);
        GC.SuppressFinalize(this);
        return true;
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken) > 0;
}