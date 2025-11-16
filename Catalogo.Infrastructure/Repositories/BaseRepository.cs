using Catalogo.Domain.Interfaces;
using Catalogo.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Infrastructure.Repositories
{
    public class BaseRepository<TEntity, TModel> : IBaseRepository<TEntity, TModel> 
        where TEntity : class
        where TModel : class
    {
        protected readonly ApplicationDbContext _context;
        private readonly Func<TModel, TEntity> _toDomain;
        private readonly Func<TEntity, TModel> _toModel;

        public BaseRepository(ApplicationDbContext context, Func<TEntity, TModel> toModel, Func<TModel, TEntity> toDomain)
        {
            _context = context;
            _toModel = toModel;
            _toDomain = toDomain;
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            var listaModel = await _context.Set<TModel>().AsNoTracking().ToListAsync();
            return listaModel.Select(_toDomain).ToList();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            var model = await _context.Set<TModel>().FindAsync(id);

            return model == null ? null : _toDomain(model);
        }

        public TEntity Create(TEntity objeto)
        {
            var model = _toModel(objeto);
            _context.Set<TModel>().Add(model);
            return objeto;
        }

        public TEntity Update(TEntity objeto)
        {
            var model = _toModel(objeto);
            _context.Set<TModel>().Update(model);
            return objeto;
        }

        public void Remove(int id)
        {
            var model = _context.Set<TModel>().Find(id);

            if (model is not null)
                _context.Set<TModel>().Remove(model);
        }
    }
}
