

namespace ZStore.Application.Interfaces
{
    public interface IRepository<T,TId>
    {
        Task<T> CreateAsync(T item);
        Task<T> GetByIdAsync(TId id);
        Task<IQueryable<T>> GetAllAsync();
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAsync(T item);
        Task<int> SaveChangesAsync();
    }
}
