using System.ComponentModel.DataAnnotations;
namespace SistemaConsultasUVV.Models;
public class Usuario
{
    public int Id { get; set; }
    [Required, StringLength(100)] public string Nome { get; set; } = string.Empty;
    [Required, EmailAddress, StringLength(150)] public string Email { get; set; } = string.Empty;
    [Required, StringLength(255)] public string SenhaHash { get; set; } = string.Empty;
    [DataType(DataType.DateTime)] public DateTime DataCadastro { get; set; } = DateTime.Now;
    public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();
}
