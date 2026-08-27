using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaConsultasUVV.Data;
using SistemaConsultasUVV.Models;
using SistemaConsultasUVV.ViewModels;
namespace SistemaConsultasUVV.Controllers;
public class AccountController(AppDbContext db, IPasswordHasher<Usuario> hasher) : Controller
{
    [HttpGet] public IActionResult Register() => View();
    [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var email = model.Email.Trim().ToLowerInvariant();
        if (await db.Usuarios.AnyAsync(u => u.Email == email)) { ModelState.AddModelError(nameof(model.Email), "Este e-mail já está cadastrado."); return View(model); }
        var user = new Usuario { Nome=model.Nome.Trim(), Email=email };
        user.SenhaHash = hasher.HashPassword(user, model.Senha); db.Usuarios.Add(user); await db.SaveChangesAsync();
        TempData["Success"] = "Cadastro realizado. Agora faça login."; return RedirectToAction(nameof(Login));
    }
    [HttpGet] public IActionResult Login(string? returnUrl = null) { ViewBag.ReturnUrl = returnUrl; return View(); }
    [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        if (!ModelState.IsValid) return View(model);
        var user = await db.Usuarios.SingleOrDefaultAsync(u => u.Email == model.Email.Trim().ToLowerInvariant());
        if (user is null || hasher.VerifyHashedPassword(user, user.SenhaHash, model.Senha) == PasswordVerificationResult.Failed) { ModelState.AddModelError(string.Empty, "E-mail ou senha inválidos."); return View(model); }
        var claims = new[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), new Claim(ClaimTypes.Name, user.Nome), new Claim(ClaimTypes.Email, user.Email) };
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)), new AuthenticationProperties { IsPersistent=model.LembrarMe });
        return LocalRedirect(returnUrl ?? Url.Action("Dashboard", "Home")!);
    }
    [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Logout() { await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); return RedirectToAction("Index", "Home"); }
}
