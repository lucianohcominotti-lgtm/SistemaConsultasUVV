using System.ComponentModel.DataAnnotations;
namespace SistemaConsultasUVV.ViewModels;
public class RegisterViewModel
{
    [Required, StringLength(100)] public string Nome { get; set; } = string.Empty;
    [Required, EmailAddress, StringLength(150)] public string Email { get; set; } = string.Empty;
    [Required, DataType(DataType.Password), StringLength(100, MinimumLength=6)] public string Senha { get; set; } = string.Empty;
    [Required, DataType(DataType.Password), Compare("Senha"), Display(Name="Confirmar senha")] public string ConfirmarSenha { get; set; } = string.Empty;
}
