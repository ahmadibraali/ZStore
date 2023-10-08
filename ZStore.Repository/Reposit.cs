

using Microsoft.EntityFrameworkCore;
using ZStore.Application.Interfaces;
using ZStore.Data;

namespace ZStore.Repository
{
    public class Reposit<T, Tid> : IRepository<T, Tid> where T : class
    {
        private readonly ZStoreContext context;
        private readonly DbSet<T> GDbSet;

        public Reposit(ZStoreContext _context)
        {
            context = _context;
            GDbSet = context.Set<T>();
        }
        public async Task<T> CreateAsync(T item)
        {
            var result = (await GDbSet.AddAsync(item)).Entity;
            await SaveChangesAsync();
            return result;
        }

        public async Task<bool> DeleteAsync(T item)
        {
            var result = GDbSet.Remove(item);
            return result!=null? true : false;
        }

        public Task<IQueryable<T>> GetAllAsync()
        {
            return (Task.FromResult(GDbSet.Select(p => p)));
        }

        public async Task<T> GetByIdAsync(Tid id)=> await GDbSet.FindAsync(id);


        public async Task<int> SaveChangesAsync()=> await context.SaveChangesAsync();

        public async Task<bool> UpdateAsync(T item)
        {
            var result = GDbSet.Update(item);
            return result !=null? true : false;
        }
    }
}
