using Catalogo.Application.DTOs;
using Catalogo.Application.Services.Interfaces;
using Catalogo.Domain.Entities;
using Catalogo.Domain.Interfaces;
using MapsterMapper;

namespace Catalogo.Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private const string CATEGORIAS_CACHE_KEY = "CategoriasCacheKey";

        public CategoriaService(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<IEnumerable<CategoriaDTO>> GetCategorias()
        {
            if (_cacheService.ExisteCache<IEnumerable<CategoriaDTO>>(CATEGORIAS_CACHE_KEY, out var categoriasCache))
                return categoriasCache;

            var categorias = await _unitOfWork.CategoriaRepository.GetAsync();
            var categoriasDTO = _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);

            _cacheService.SetCache<IEnumerable<CategoriaDTO>>(CATEGORIAS_CACHE_KEY, categoriasDTO);

            return categoriasDTO;
        }

        public async Task<CategoriaDTO> GetById(int id)
        {
            var categoriaCacheKey = _cacheService.GetCategoriasCacheKey(CATEGORIAS_CACHE_KEY, id);

            if (_cacheService.ExisteCache<CategoriaDTO>(categoriaCacheKey, out var categoriaCache))
                return categoriaCache;

            var categoria = await _unitOfWork.CategoriaRepository.GetByIdAsync(id);
            var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

            _cacheService.SetCache<CategoriaDTO>(categoriaCacheKey, categoriaDTO);

            return categoriaDTO;
        }

        public async Task<CategoriaDTO> Create(CategoriaDTO categoriaDTO)
        {
            categoriaDTO.Id = 0;
            var categoria = _mapper.Map<Categoria>(categoriaDTO);
            var categoriaCriada = _unitOfWork.CategoriaRepository.Create(categoria);
            await _unitOfWork.CommitAsync();

            _cacheService.LimparCache(CATEGORIAS_CACHE_KEY);
            _cacheService.SetCache<CategoriaDTO>(_cacheService.GetCategoriasCacheKey(CATEGORIAS_CACHE_KEY, categoriaCriada.Id), categoriaDTO);

            return _mapper.Map<CategoriaDTO>(categoriaCriada);
        }

        public async Task<bool> Remove(int id)
        {
            _unitOfWork.CategoriaRepository.Remove(id);
            var retorno = await _unitOfWork.CommitAsync();

            _cacheService.RemoverCache(CATEGORIAS_CACHE_KEY, id);

            return retorno > 0;
        }

        public async Task<CategoriaDTO> Update(CategoriaDTO categoriaDTO)
        {
            var categoria = _mapper.Map<Categoria>(categoriaDTO);
            var categoriaAtualizada = _unitOfWork.CategoriaRepository.Update(categoria);
            await _unitOfWork.CommitAsync();

            _cacheService.LimparCache(CATEGORIAS_CACHE_KEY);
            _cacheService.SetCache<CategoriaDTO>(_cacheService.GetCategoriasCacheKey(CATEGORIAS_CACHE_KEY, categoriaAtualizada.Id), categoriaDTO);

            return _mapper.Map<CategoriaDTO>(categoriaAtualizada);
        }
    }
}