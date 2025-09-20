namespace Catalogo.Domain.Entities;

// sealed impede que outras classes herdem dela
public sealed class Categoria : Entity 
{
    public string? Nome { get; private set; }
    public string? ImagemUrl { get; private set; }
    public DateTime DataCadastro { get; set; }
    public ICollection<Produto> Produtos { get; set; }
}
