using Catalogo.Domain.Entities;

namespace Catalogo.Infrastructure.Models
{
    public class CategoriaModel : Entity
    {
        public string? Nome { get; set; }
        public string? ImagemUrl { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
