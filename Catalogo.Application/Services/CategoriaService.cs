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

        public CategoriaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoriaDTO>> GetCategorias()
        {
            var categorias = await _unitOfWork.CategoriaRepository.GetAsync();
            var categoriasDTO = _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);
            return categoriasDTO;
        }

        public async Task<CategoriaDTO> GetById(int id)
        {
            var categoria = await _unitOfWork.CategoriaRepository.GetByIdAsync(id);
            var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);
            return categoriaDTO;
        }

        public async Task<CategoriaDTO> Create(CategoriaDTO categoriaDTO)
        {
            var categoria = _mapper.Map<Categoria>(categoriaDTO);
            var categoriaCriada = _unitOfWork.CategoriaRepository.Create(categoria);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<CategoriaDTO>(categoriaCriada);
        }

        public async Task<bool> Remove(int id)
        {
            _unitOfWork.CategoriaRepository.Remove(id);
            var retorno = await _unitOfWork.CommitAsync();

            return retorno > 0;
        }

        public async Task<CategoriaDTO> Update(CategoriaDTO categoriaDTO)
        {
            var categoria = _mapper.Map<Categoria>(categoriaDTO);
            var categoriaAtualizada = _unitOfWork.CategoriaRepository.Update(categoria);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<CategoriaDTO>(categoriaAtualizada);
        }
    }
}