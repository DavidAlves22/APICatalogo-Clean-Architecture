namespace Catalogo.Domain.Interfaces;

public interface IUnitOfWork
{
    IProdutoRepository ProdutoRepository { get; }
    ICategoriaRepository CategoriaRepository { get; }
    Task<int> CommitAsync();
}