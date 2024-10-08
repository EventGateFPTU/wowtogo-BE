using Domain.Models;

namespace Domain.Interfaces.Data.IRepositories;

public interface ILikeEventRepository : IRepositoryBase<LikeEvent>
{
    Task<LikeEvent?> GetEventLike(Guid userId, Guid eventId, CancellationToken cancellationToken = default);
    Task<List<LikeEvent>> GetEventLikes(Guid eventId, CancellationToken cancellationToken = default);
}