using SistemaConsultasUVV.Models;

namespace SistemaConsultasUVV.ViewModels;

public class DashboardViewModel
{
    public int TotalConsultas { get; set; }

    public int ConsultasFuturas { get; set; }

    public Consulta? ProximaConsulta { get; set; }

    public List<Consulta> ProximasConsultas { get; set; } = new();
}