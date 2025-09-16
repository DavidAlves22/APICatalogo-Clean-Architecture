using Catalogo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalogo.Infrastructure.EntitiesConfiguration;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Nome).IsRequired().HasMaxLength(80);
        builder.Property(p => p.Descricao).IsRequired().HasMaxLength(300);
        builder.Property(p => p.Preco).IsRequired().HasPrecision(10, 2);
        builder.Property(p => p.ImagemUrl).IsRequired().HasMaxLength(300);
        builder.Property(p => p.Estoque).IsRequired().HasDefaultValue(1);
        builder.Property(p => p.DataCadastro).IsRequired().HasDefaultValueSql("GETDATE()");

        builder.HasOne(e => e.Categoria).WithMany(e => e.Produtos).HasForeignKey(p => p.CategoriaId);
    }
}
