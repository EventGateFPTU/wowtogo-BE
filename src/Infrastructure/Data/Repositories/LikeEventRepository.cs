using Domain.Interfaces.Data.IRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class LikeEventRepository(WowToGoDBContext context) : RepositoryBase<LikeEvent>(context),ILikeEventRepository
{
    public async Task<LikeEvent?> GetEventLike(Guid userId, Guid eventId, CancellationToken cancellationToken = default)
        => await _dbSet.AsNoTracking()
            .Where(e => e.UserId == userId && e.EventId == eventId)
            .FirstOrDefaultAsync(cancellationToken);
    
    public async Task<List<LikeEvent>> GetEventLikes(Guid eventId, CancellationToken cancellationToken = default)
        => await _dbSet.AsNoTracking()
            .Where(e => e.EventId == eventId)
            .ToListAsync(cancellationToken);
    
}