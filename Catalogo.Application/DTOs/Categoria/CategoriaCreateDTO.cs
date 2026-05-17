using System.ComponentModel.DataAnnotations;

namespace Catalogo.Application.DTOs.Categoria;

public class CategoriaCreateDTO
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [MinLength(3)]
    [MaxLength(80)]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "A imagem url é obrigatória")]
    [MinLength(5)]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }
}
