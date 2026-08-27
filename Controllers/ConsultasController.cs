using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaConsultasUVV.Data;
using SistemaConsultasUVV.Models;

namespace SistemaConsultasUVV.Controllers;

[Authorize]
public class ConsultasController : Controller
{
    private readonly AppDbContext _db;

    private static readonly string[] StatusPermitidos =
    {
        "Agendada",
        "Realizada",
        "Cancelada"
    };

    public ConsultasController(AppDbContext db)
    {
        _db = db;
    }

    private int UsuarioId
    {
        get
        {
            var identificador = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            return int.Parse(identificador!);
        }
    }

    public async Task<IActionResult> Index(
        string? busca,
        string? status)
    {
        var consultaQuery = _db.Consultas
            .Where(c => c.UsuarioId == UsuarioId);

        if (!string.IsNullOrWhiteSpace(busca))
        {
            busca = busca.Trim();

            consultaQuery = consultaQuery.Where(c =>
                c.Especialidade.Contains(busca) ||
                c.Descricao.Contains(busca));
        }

        if (!string.IsNullOrWhiteSpace(status) &&
            status != "Todos" &&
            StatusPermitidos.Contains(status))
        {
            consultaQuery = consultaQuery.Where(c =>
                c.Status == status);
        }

        var consultas = await consultaQuery
            .OrderBy(c => c.DataHora)
            .ToListAsync();

        ViewBag.Busca = busca;
        ViewBag.StatusSelecionado = status ?? "Todos";

        return View(consultas);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var consulta = await ObterConsultaDoUsuario(id);

        if (consulta == null)
        {
            return NotFound();
        }

        return View(consulta);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var consulta = new Consulta
        {
            DataHora = DateTime.Now
                .AddDays(1)
                .AddMinutes(-DateTime.Now.Minute),
            Status = "Agendada"
        };

        return View(consulta);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Consulta consulta)
    {
        if (!StatusPermitidos.Contains(consulta.Status))
        {
            consulta.Status = "Agendada";
        }

        if (consulta.DataHora < DateTime.Now)
        {
            ModelState.AddModelError(
                nameof(consulta.DataHora),
                "A consulta deve ser agendada para uma data futura.");
        }

        var horarioOcupado = await _db.Consultas.AnyAsync(c =>
            c.UsuarioId == UsuarioId &&
            c.DataHora == consulta.DataHora &&
            c.Status != "Cancelada");

        if (horarioOcupado)
        {
            ModelState.AddModelError(
                nameof(consulta.DataHora),
                "Você já possui uma consulta neste horário.");
        }

        if (!ModelState.IsValid)
        {
            return View(consulta);
        }

        consulta.UsuarioId = UsuarioId;

        _db.Consultas.Add(consulta);
        await _db.SaveChangesAsync();

        TempData["Success"] = "Consulta cadastrada com sucesso.";

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var consulta = await ObterConsultaDoUsuario(id);

        if (consulta == null)
        {
            return NotFound();
        }

        return View(consulta);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        Consulta consulta)
    {
        if (id != consulta.Id)
        {
            return NotFound();
        }

        if (!StatusPermitidos.Contains(consulta.Status))
        {
            consulta.Status = "Agendada";
        }

        if (consulta.DataHora < DateTime.Now &&
            consulta.Status != "Realizada")
        {
            ModelState.AddModelError(
                nameof(consulta.DataHora),
                "A consulta deve ser futura, exceto quando estiver realizada.");
        }

        var horarioOcupado = await _db.Consultas.AnyAsync(c =>
            c.UsuarioId == UsuarioId &&
            c.Id != consulta.Id &&
            c.DataHora == consulta.DataHora &&
            c.Status != "Cancelada");

        if (horarioOcupado)
        {
            ModelState.AddModelError(
                nameof(consulta.DataHora),
                "Você já possui outra consulta neste horário.");
        }

        if (!ModelState.IsValid)
        {
            return View(consulta);
        }

        var consultaExistente = await ObterConsultaDoUsuario(id);

        if (consultaExistente == null)
        {
            return NotFound();
        }

        consultaExistente.Especialidade = consulta.Especialidade;
        consultaExistente.DataHora = consulta.DataHora;
        consultaExistente.Descricao = consulta.Descricao;
        consultaExistente.Status = consulta.Status;

        await _db.SaveChangesAsync();

        TempData["Success"] = "Consulta atualizada com sucesso.";

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var consulta = await ObterConsultaDoUsuario(id);

        if (consulta != null)
        {
            _db.Consultas.Remove(consulta);
            await _db.SaveChangesAsync();

            TempData["Success"] = "Consulta excluída com sucesso.";
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task<Consulta?> ObterConsultaDoUsuario(int id)
    {
        return await _db.Consultas
            .SingleOrDefaultAsync(c =>
                c.Id == id &&
                c.UsuarioId == UsuarioId);
    }
}