
namespace TMS.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        int SaveChanges();
        void Detach(object entity);
        void Dispose();
        Task<int> SaveChangesAsync();
    }
}
