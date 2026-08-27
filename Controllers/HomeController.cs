using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaConsultasUVV.Data;
using SistemaConsultasUVV.Models;
using SistemaConsultasUVV.ViewModels;

namespace SistemaConsultasUVV.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _db;

    public HomeController(AppDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    public async Task<IActionResult> Dashboard()
    {
        var identificador = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        var usuarioId = int.Parse(identificador!);
        var agora = DateTime.Now;

        var consultas = _db.Consultas
            .Where(c => c.UsuarioId == usuarioId);

        var consultasFuturas = await consultas
            .Where(c => c.DataHora >= agora)
            .OrderBy(c => c.DataHora)
            .ToListAsync();

        var model = new DashboardViewModel
        {
            TotalConsultas = await consultas.CountAsync(),
            ConsultasFuturas = consultasFuturas.Count,
            ProximaConsulta = consultasFuturas.FirstOrDefault(),
            ProximasConsultas = consultasFuturas
                .Take(5)
                .ToList()
        };

        return View(model);
    }

    [ResponseCache(
        Duration = 0,
        Location = ResponseCacheLocation.None,
        NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }

    public IActionResult StatusCode(int code)
    {
        ViewBag.StatusCode = code;

        return View();
    }
}