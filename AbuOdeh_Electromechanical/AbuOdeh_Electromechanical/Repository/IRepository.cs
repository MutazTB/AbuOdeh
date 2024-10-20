namespace AbuOdeh_Electromechanical.Repository
{
    public interface IRepository<T> where T : Entity
    {
        IUnitOfWork UnitOfWork { get; }
        Task<T> GetById(int id);
        Task Delete(int id);
        Task<T> GetById(Guid objectKey);
        Task<T> Add(T entity);
        IQueryable<T> Queryable();
    }
}
