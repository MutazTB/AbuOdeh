using System.Data;

namespace AbuOdeh_Electromechanical.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        Task BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default);
        Task Commit(CancellationToken cancellationToken = default);
        Task Rollback(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task ExecuteSql(string sql, CancellationToken cancellationToken = default);
    }
}
