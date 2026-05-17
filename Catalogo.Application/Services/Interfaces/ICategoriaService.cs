using Catalogo.Application.DTOs.Categoria;

namespace Catalogo.Application.Services.Interfaces;

public interface ICategoriaService
{
    Task<IEnumerable<CategoriaDTO>> GetCategorias();
    Task<CategoriaDTO?> GetById(int id);
    Task<CategoriaDTO> Create(CategoriaCreateDTO categoriaDTO);
    Task<CategoriaDTO> Update(CategoriaDTO categoriaDTO);
    Task<bool> Remove(int id);
}
