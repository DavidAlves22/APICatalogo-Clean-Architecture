using Catalogo.Domain.Validation;

namespace Catalogo.Domain.Entities
{
    public sealed class Produto : Entity
    {
        public Produto(string nome, string descricao, decimal preco, string imagemUrl, int estoque, DateTime dataCadastro)
        {
            ValidateDomain(nome, descricao, preco, imagemUrl, estoque, dataCadastro);
        }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public string ImagemUrl { get; set; }
        public int Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public void Update(string nome, string descricao, decimal preco, string imagemUrl, int estoque, DateTime dataCadastro, int categoriaId)
        {
            ValidateDomain(nome, descricao, preco, imagemUrl, estoque, dataCadastro);
            CategoriaId = categoriaId;
        }

        public void ValidateDomain(string nome, string descricao, decimal preco, string imagemUrl, int estoque, DateTime dataCadastro)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome é obrigatório.");
            DomainExceptionValidation.When(nome.Length < 3, "Nome deve ter no mínimo 3 caracteres.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(descricao), "Descrição é obrigatória.");
            DomainExceptionValidation.When(descricao.Length < 5, "Descrição deve ter no mínimo 5 caracteres.");

            DomainExceptionValidation.When(preco < 0, "Preço não pode ser negativo.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(imagemUrl), "Imagem URL é obrigatória.");
            DomainExceptionValidation.When(imagemUrl?.Length < 250, "Imagem deve ter no mínimo 5 caracteres.");

            DomainExceptionValidation.When(estoque < 0, "Estoque não pode ser negativo.");

            DomainExceptionValidation.When(dataCadastro == DateTime.MinValue, "Data de cadastro inválida.");
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            ImagemUrl = imagemUrl;
            Estoque = estoque;
            DataCadastro = dataCadastro;
        }
    }
}
