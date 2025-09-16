using Catalogo.Domain.Interfaces;
using Catalogo.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public T Create(T objeto)
        {
            _context.Set<T>().Add(objeto);
            return objeto;
        }

        public T Update(T objeto)
        {
            _context.Set<T>().Update(objeto);
            return objeto;
        }

        public void Remove(int id)
        {
            var objeto = _context.Set<T>().Find(id);

            if (objeto is not null)
                _context.Set<T>().Remove(objeto);
        }
    }
}
