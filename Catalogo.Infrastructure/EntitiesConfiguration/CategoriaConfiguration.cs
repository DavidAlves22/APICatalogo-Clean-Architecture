using Catalogo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalogo.Infrastructure.EntitiesConfiguration;

public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Nome).IsRequired().HasMaxLength(80);
        builder.Property(c => c.ImagemUrl).IsRequired().HasMaxLength(300);
    }
}
