using Catalogo.Application.DTOs;

namespace Catalogo.Application.Services.Interfaces;

public interface ICategoriaService
{
    Task<IEnumerable<CategoriaDTO>> GetCategorias();
    Task<CategoriaDTO> GetById(int id);
    Task<CategoriaDTO> Create(CategoriaDTO categoriaDTO);
    Task<CategoriaDTO> Update(CategoriaDTO categoriaDTO);
    Task<bool> Remove(int id);
}
