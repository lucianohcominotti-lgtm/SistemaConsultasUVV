using System.ComponentModel.DataAnnotations;

namespace SistemaConsultasUVV.Models;

public class Consulta
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Especialidade { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Data e hora")]
    [DataType(DataType.DateTime)]
    public DateTime DataHora { get; set; }

    [Required]
    [StringLength(1000)]
    [Display(Name = "Descrição")]
    public string Descricao { get; set; } = string.Empty;

    [Required]
    [StringLength(30)]
    public string Status { get; set; } = "Agendada";

    [Required]
    public int UsuarioId { get; set; }

    public Usuario? Usuario { get; set; }
}