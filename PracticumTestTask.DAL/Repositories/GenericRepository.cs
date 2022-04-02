using Microsoft.EntityFrameworkCore;
using PracticumTestTask.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PracticumTestTask.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly WordDbContext _context;

        public GenericRepository(WordDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(T item)
        {
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(List<T> items)
        {
            await _context.AddRangeAsync(items);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> ExistsAsync(Guid id)
        {
            var dbSet = _context.Set<T>();
            var item = await dbSet.FindAsync(id);

            if (item == null)
                return false;

            _context.Entry(item).State = EntityState.Detached;
            return true;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task DeleteAsync(T item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task UpdateAsync(T item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(List<T> items)
        {
            _context.UpdateRange(items);
            await _context.SaveChangesAsync();
        }

    }
}
