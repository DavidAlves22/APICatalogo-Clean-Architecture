using Catalogo.Domain.Validation;

namespace Catalogo.Domain.Entities;

// sealed impede que outras classes herdem dela
public sealed class Categoria : Entity 
{
    public Categoria(string? nome, string? imagemUrl)
    {
        ValidateDomain(nome, imagemUrl);
    }

    public Categoria(int id, string? nome, string? imagemUrl)
    {
        DomainExceptionValidation.When(id < 0, "Valor Id inválido.");
        Id = id;
        ValidateDomain(nome, imagemUrl);
    }

    // Definir set como private para evitar que o Id seja alterado externamente (Apenas no contrutor)

    public string? Nome { get; private set; }
    public string? ImagemUrl { get; private set; }
    public ICollection<Produto> Produtos { get; set; }

    public void ValidateDomain(string nome, string imagemUrl)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome é obrigatório.");

        DomainExceptionValidation.When(nome.Length < 3, "Nome deve ter no mínimo 3 caracteres.");

        DomainExceptionValidation.When(string.IsNullOrEmpty(imagemUrl), "Imagem URL é obrigatório.");

        DomainExceptionValidation.When(imagemUrl!.Length < 5, "Imagem deve ter no mínimo 5 caracteres.");

        Nome = nome;
        ImagemUrl = imagemUrl;
    }
}
