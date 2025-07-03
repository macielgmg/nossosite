using Microsoft.AspNetCore.Mvc;
using MeuSiteLogin.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class LoginController : Controller
{
    private readonly AppDbContext _context;

    public LoginController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        if (HttpContext.Session.GetInt32("UsuarioId") != null)
        {
            return RedirectToAction("Index", "Dashboard");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(string usuario, string senha)
    {
        var user = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.UsuarioLogin == usuario && u.Senha == senha);

        if (user != null)
        {
            HttpContext.Session.SetInt32("UsuarioId", user.Id);
            return RedirectToAction("Index", "Dashboard");
        }

        ViewBag.Erro = "Usuário ou senha inválidos.";
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }


}
