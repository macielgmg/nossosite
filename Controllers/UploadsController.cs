using Microsoft.AspNetCore.Mvc;
using MeuSiteLogin.Data;
using MeuSiteLogin.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class UploadsController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public UploadsController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var userId = HttpContext.Session.GetInt32("UsuarioId");

        if (userId == null)
            return RedirectToAction("Index", "Login");

        var arquivos = _context.Arquivos
            .Where(a => a.UsuarioId == userId)
            .OrderByDescending(a => a.DataEnvio)
            .Take(10)
            .ToList();

        ViewBag.Arquivos = arquivos;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(IFormFile arquivo)
    {
        var userId = HttpContext.Session.GetInt32("UsuarioId");

        if (userId == null)
            return RedirectToAction("Index", "Login");

        if (arquivo == null || arquivo.Length == 0)
        {
            ViewBag.Mensagem = "Selecione um arquivo válido.";
            return RedirectToAction("Index");
        }

        var uploadsPath = Path.Combine(_env.WebRootPath, "uploads");
        if (!Directory.Exists(uploadsPath))
            Directory.CreateDirectory(uploadsPath);

        var fileName = Path.GetFileName(arquivo.FileName);
        var filePath = Path.Combine(uploadsPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await arquivo.CopyToAsync(stream);
        }

        var novoArquivo = new Arquivo
        {
            NomeOriginal = fileName,
            Caminho = "/uploads/" + fileName,
            UsuarioId = userId.Value
        };

        _context.Arquivos.Add(novoArquivo);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}
