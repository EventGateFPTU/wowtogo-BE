using Domain.Interfaces.Data.IRepositories;

namespace Domain.Interfaces.Data;
public interface IUnitOfWork : IDisposable
{
    IArticleRepository ArticleRepository { get; }
    IAttendeeRepository AttendeeRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    IEventCategoryRepository EventCategoryRepository { get; }
    IEventRepository EventRepository { get; }
    IOrderRepository OrderRepository { get; }
    IOrganizerRepository OrganizerRepository { get; }
    IShowRepository ShowRepository { get; }
    IStaffRepository StaffRepository { get; }
    ITicketRepository TicketRepository { get; }
    ITicketTypeRepository TicketTypeRepository { get; }
    IUserRepository UserRepository { get; }
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
}