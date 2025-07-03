using Microsoft.AspNetCore.Mvc;
using MeuSiteLogin.Data;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

public class ConfiguracoesController : Controller
{
    private readonly AppDbContext _context;

    public ConfiguracoesController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var userId = HttpContext.Session.GetInt32("UsuarioId");
        if (userId == null) return RedirectToAction("Index", "Login");

        var usuario = await _context.Usuarios.FindAsync(userId);
        return View(usuario);
    }

    [HttpPost]
    public async Task<IActionResult> AlterarEmail(string novoEmail)
    {
        var userId = HttpContext.Session.GetInt32("UsuarioId");
        if (userId == null) return RedirectToAction("Index", "Login");

        var usuario = await _context.Usuarios.FindAsync(userId);
        usuario.Email = novoEmail;
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> AlterarSenha(string novaSenha)
    {
        var userId = HttpContext.Session.GetInt32("UsuarioId");
        if (userId == null) return RedirectToAction("Index", "Login");

        var usuario = await _context.Usuarios.FindAsync(userId);
        usuario.Senha = novaSenha;
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> AlterarFoto(IFormFile foto)
    {
        var userId = HttpContext.Session.GetInt32("UsuarioId");
        if (userId == null || foto == null) return RedirectToAction("Index", "Login");

        using var ms = new MemoryStream();
        await foto.CopyToAsync(ms);
        var fotoBytes = ms.ToArray();

        var usuario = await _context.Usuarios.FindAsync(userId);
        usuario.Foto = fotoBytes;
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}
