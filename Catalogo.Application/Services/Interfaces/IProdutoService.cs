using Catalogo.Application.DTOs.Produto;

namespace Catalogo.Application.Services.Interfaces;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoDTO>> GetProdutos();
    Task<ProdutoDTO?> GetById(int id);
    Task<ProdutoDTO> Create(ProdutoCreateDTO produtoDTO);
    Task<ProdutoDTO> Update(ProdutoDTO produtoDTO);
    Task<bool> Remove(int id);
}
