namespace Catalogo.Domain.Entities;

// sealed impede que outras classes herdem dela
public sealed class Categoria : Entity
{
    public string? Nome { get; private set; }
    public string? ImagemUrl { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public ICollection<Produto> Produtos { get; private set; } = new List<Produto>();

    public Categoria(int id, string nome, string imagemUrl)
    {
        Id = id;
        SetNome(nome);
        SetImagemUrl(imagemUrl);
        DefinirDataCadastro();
    }

    private void SetNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome da categoria não pode ser vazio.");

        Nome = nome;
    }

    private void SetImagemUrl(string imagemUrl)
    {
        if (string.IsNullOrWhiteSpace(imagemUrl))
            throw new ArgumentException("ImagemUrl da categoria não pode ser vazio.");

        ImagemUrl = imagemUrl;
    }

    private void DefinirDataCadastro()
    {
        DataCadastro = DateTime.UtcNow;
    }

    public void AdicionarProduto(Produto produto)
    {
        if (produto == null)
            throw new ArgumentException("Produto não pode ser nulo.");

        Produtos.Add(produto);
    }
}
