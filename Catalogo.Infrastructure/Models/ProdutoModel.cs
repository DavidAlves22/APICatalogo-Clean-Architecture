using Catalogo.Domain.Entities;

namespace Catalogo.Infrastructure.Models;

public class ProdutoModel : Entity
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public string ImagemUrl { get; set; }
    public int Estoque { get; set; }
    public DateTime DataCadastro { get; set; }
    public int CategoriaId { get; set; }
}
