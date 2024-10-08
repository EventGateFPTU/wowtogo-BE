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
    ITicketTypeShowRepository TicketTypeShowRepository { get; }
    IUserRepository UserRepository { get; }
    ICheckinRepository CheckinRepository { get; }
    IShowStaffRepository ShowStaffRepository { get; }
    IAdditionalImageRepository AdditionalImageRepository { get; }
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
}