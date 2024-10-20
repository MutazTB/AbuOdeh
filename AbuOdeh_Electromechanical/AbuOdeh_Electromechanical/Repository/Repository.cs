
using AbuOdeh_Electromechanical.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace AbuOdeh_Electromechanical.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        public Repository(DbContext context)
        {
            Context = context;
        }
        public IUnitOfWork UnitOfWork => Context as IUnitOfWork;
        public DbContext Context { get; }

        public Task<T> GetById(int id)
        {
            return AsQueryable().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> Add(T entity)
        {
            return (await Context.Set<T>().AddAsync(entity)).Entity;
        }

        protected virtual IQueryable<T> AsQueryable()
        {
            var queryable = Context.Set<T>().AsQueryable();

            foreach (var navigation in Context.Model.FindEntityType(typeof(T)).GetNavigations())
                queryable = queryable.Include(navigation.Name);

            return queryable;
        }

        public IQueryable<T> Queryable()
        {
            return Context.Set<T>().AsQueryable<T>();
        }

        public Task<T> GetById(Guid objectKey)
        {
            return AsQueryable().SingleOrDefaultAsync(x => x.ObjectKey == objectKey);
        }

        public async Task Delete(int id)
        {
            var result = await AsQueryable().SingleOrDefaultAsync(x => x.Id == id);
            result.IsDeleted = true;
            await Context.SaveChangesAsync();
        }
    }
}
