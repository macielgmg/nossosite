// Controllers/LoginController.cs
using Microsoft.AspNetCore.Mvc;
using MeuSiteLogin.Models;
using MeuSiteLogin.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class LoginController : Controller
{
    private readonly AppDbContext _context;

    public LoginController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index() => View();

    [HttpPost]
    public async Task<IActionResult> Index(string usuario, string senha)
    {
        var user = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.UsuarioLogin == usuario && u.Senha == senha);

        if (user != null)
        {
            // Aqui você pode usar sessões ou cookies
            HttpContext.Session.SetInt32("UsuarioId", user.Id);
            return RedirectToAction("Index", "Dashboard");
        }

        ViewBag.Erro = "Usuário ou senha inválidos.";
        return View();
    }
}
