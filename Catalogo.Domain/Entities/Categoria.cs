namespace Catalogo.Domain.Entities;

// sealed impede que outras classes herdem dela
public sealed class Categoria : Entity
{
    public string? Nome { get; private set; }
    public string? ImagemUrl { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public ICollection<Produto> Produtos { get; private set; }

    public Categoria(int id, string nome, string imagemUrl)
    {
        Id = id;
        Nome = nome;
        ImagemUrl = imagemUrl;
        DataCadastro = DateTime.UtcNow;
    }
}
