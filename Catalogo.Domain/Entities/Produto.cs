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
            SetNome(nome);
            SetDescricao(descricao);
            SetPreco(preco);
            SetImagemUrl(imagemUrl);
            SetEstoque(estoque);
            SetCategoriaId(categoriaId);
            DefinirDataCadastro();
        }

        private void SetNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome do produto não pode ser vazio.");

            Nome = nome;
        }

        private void SetDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("Descrição do produto não pode ser vazia.");

            Descricao = descricao;
        }

        private void SetPreco(decimal preco)
        {
            if (preco < 0)
                throw new ArgumentException("Preço do produto não pode ser negativo.");

            Preco = preco;
        }

        private void SetImagemUrl(string imagemUrl)
        {
            if (string.IsNullOrWhiteSpace(imagemUrl))
                throw new ArgumentException("ImagemUrl do produto não pode ser vazia.");

            ImagemUrl = imagemUrl;
        }

        private void SetEstoque(int estoque)
        {
            if (estoque < 0)
                throw new ArgumentException("Estoque do produto não pode ser negativo.");

            Estoque = estoque;
        }

        private void SetCategoriaId(int categoriaId)
        {
            if (categoriaId <= 0)
                throw new ArgumentException("CategoriaId deve ser maior que zero.");

            CategoriaId = categoriaId;
        }

        private void DefinirDataCadastro()
        {
            DataCadastro = DateTime.UtcNow;
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

            if (quantidade > Estoque)
                throw new ArgumentException("Quantidade a remover não pode ser maior que o estoque atual.");

            Estoque -= quantidade;
        }

        public void AtualizarCategoria(int novaCategoriaId)
        {
            if (novaCategoriaId <= 0)
                throw new ArgumentException("CategoriaId deve ser maior que zero.");

            CategoriaId = novaCategoriaId;
        }
    }
}
