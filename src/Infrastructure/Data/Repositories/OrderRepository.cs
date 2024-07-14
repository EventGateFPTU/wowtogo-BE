namespace Infrastructure.Data.Repositories;
public class OrderRepository(WowToGoDBContext context) : RepositoryBase<Order>(context), IOrderRepository
{
    public async Task<PaginatedResponse<OrderResponse>> GetOrdersByEventAsync(Guid eventId, int pageNumber = 1, int pageSize = 10, bool trackChanges = false, CancellationToken cancellationToken = default)
    {
        IQueryable<Order> query = _dbSet;
        if (!trackChanges) query = query.AsNoTracking();
        query = query.Include(o => o.TicketType)
                    .ThenInclude(tt => tt.TicketTypeShows)
                    .ThenInclude(tts => tts.Show)
                    .ThenInclude(s => s.Event)
                    .Where(o => o.TicketType.TicketTypeShows.Any(tts => tts.Show.Event.Id.Equals(eventId)));
        int count = query.Count();
        IEnumerable<OrderResponse> result = await query.Skip((pageNumber - 1) * pageSize)
                                                        .Take(pageSize)
                                                        .Select(o => o.MapToOrderResponse())
                                                        .ToListAsync(cancellationToken);
        return new PaginatedResponse<OrderResponse>(
            Data: result,
            PageNumber: pageNumber,
            PageSize: pageSize,
            Count: count
        );
    }

    public async Task<PaginatedResponse<PaidOrderDB>> GetPaidOrdersAsync(Guid userId, int pageNumber = 1, int pageSize = 10, bool trackChanges = false, CancellationToken cancellationToken = default)
    {
        IQueryable<Order> query = _dbSet;
        if (!trackChanges) query = query.AsNoTracking();
        int count = await query.Where(o => o.Status == OrderStatusEnum.Paid && o.UserId == userId).CountAsync(cancellationToken);
        IEnumerable<PaidOrderDB> result = await query.Where(o => o.Status == OrderStatusEnum.Paid && o.UserId == userId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(o => o.MapToPaidOrderDB())
            .ToListAsync(cancellationToken);
        return new PaginatedResponse<PaidOrderDB>(
            Data: result,
            PageNumber: pageNumber,
            PageSize: pageSize,
            Count: count
            );
    }

    public async Task<PaginatedResponse<PendingOrderDB>> GetPendingOrdersAsync(Guid userId, int pageNumber = 1, int pageSize = 10, bool trackChanges = false, CancellationToken cancellationToken = default)
    {
        IQueryable<Order> query = _dbSet;
        if (!trackChanges) query = query.AsNoTracking();
        int count = await query.Where(o => o.Status == OrderStatusEnum.Pending && o.UserId == userId).CountAsync(cancellationToken);
        IEnumerable<PendingOrderDB> result = await query.Where(o => o.Status == OrderStatusEnum.Pending && o.UserId == userId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(o => o.MapToPendingOrderDB())
            .ToListAsync(cancellationToken);
        return new PaginatedResponse<PendingOrderDB>(
                       Data: result,
                       PageNumber: pageNumber,
                       PageSize: pageSize,
                       Count: count
                       );
    }
}