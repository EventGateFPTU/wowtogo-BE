using Domain.Enums;
using Domain.Interfaces.Data.IRepositories;
using Domain.Models;
using Domain.Responses.Responses_Order;
using Domain.Responses.Shared;
using Microsoft.EntityFrameworkCore;
using UseCases.Mapper.Mapper_Order;

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

    public async Task<IEnumerable<OrderResponse>> GetPaidOrdersAsync(Guid userId, int pageNumber = 1, int pageSize = 10, bool trackChanges = false, CancellationToken cancellationToken = default)
    {
        IQueryable<Order> query = _dbSet;
        if (!trackChanges) query = query.AsNoTracking();
        return await query.Where(o => o.Status == OrderStatusEnum.Paid && o.UserId == userId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(o => o.MapToOrderResponse())
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<OrderResponse>> GetPendingOrdersAsync(Guid userId, int pageNumber = 1, int pageSize = 10, bool trackChanges = false, CancellationToken cancellationToken = default)
    {
        IQueryable<Order> query = _dbSet;
        if (!trackChanges) query = query.AsNoTracking();
        return await query.Where(o => o.Status == OrderStatusEnum.Pending && o.UserId == userId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(o => o.MapToOrderResponse())
            .ToListAsync(cancellationToken);
    }
}