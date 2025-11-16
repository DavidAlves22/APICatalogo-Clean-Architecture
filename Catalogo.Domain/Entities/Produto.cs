namespace Catalogo.Domain.Entities
{
    public sealed class Produto : Entity
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }
        public string ImagemUrl { get; private set; }
        public int Estoque { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public int CategoriaId { get; private set; }
        public Categoria Categoria { get; private set; }

        public Produto(int id, string nome, string descricao, decimal preco, string imagemUrl, int estoque, int categoriaId)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            ImagemUrl = imagemUrl;
            Estoque = estoque;
            DataCadastro = DateTime.UtcNow;
            CategoriaId = categoriaId;
        }

        public void AdicionarEstoque(int quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero.");

            Estoque += quantidade;
        }

        public void RemoverEstoque(int quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero.");

            Estoque -= quantidade;
        }

        public void AtualizarCategoria(int novaCategoriaId)
        {
            if(novaCategoriaId <= 0)
                throw new ArgumentException("CategoriaId deve ser maior que zero.");

            CategoriaId = novaCategoriaId;
        }
    }
}
