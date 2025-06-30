// Controllers/DashboardController.cs
using Microsoft.AspNetCore.Mvc;
using MeuSiteLogin.Data;
using MeuSiteLogin.Models;

public class DashboardController : Controller
{
    private readonly AppDbContext _context;

    public DashboardController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var userId = HttpContext.Session.GetInt32("UsuarioId");

        if (userId == null)
            return RedirectToAction("Index", "Login");

        var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == userId.Value);

        if (usuario == null)
            return RedirectToAction("Index", "Login");

        return View(usuario); // passa o objeto completo para a view
    }
}
