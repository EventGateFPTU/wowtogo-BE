using Domain.Interfaces.Data;

namespace Infrastructure.Data;
public class UnitOfWork(WowToGoDBContext context) : IUnitOfWork
{
    private readonly WowToGoDBContext _context = context;
    private bool _disposed = true;
    public void Dispose()
    {
        if (_disposed) _disposed = !Dispose(true).Result;
    }

    public async Task<bool> Dispose(bool disposing)
    {
        if (disposing)
        {
            await _context.DisposeAsync();
            GC.SuppressFinalize(this);
            return true;
        }
        return false;
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken) > 0;
}