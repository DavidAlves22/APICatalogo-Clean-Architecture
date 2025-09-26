using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalogo.Application.DTOs;

public class ProdutoDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage ="Nome é obrigatório")]
    [MinLength(3)]
    [MaxLength(80)]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "Descricao é obrigatória")]
    [MinLength(5)]
    [MaxLength(300)]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "Preço é obrigatório")]
    [Column(TypeName = "decimal(10,2)")]
    [DisplayFormat(DataFormatString = "{0:C2}")]
    [DataType(DataType.Currency)]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "A imagem url é obrigatória")]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }

    [Required(ErrorMessage = "Estoque é obrigatório")]
    [Range(1,99999)]
    public int Estoque { get; set; }

    public DateTime DataCadastro { get; set; }

    public int CategoriaId { get; set; }
}
