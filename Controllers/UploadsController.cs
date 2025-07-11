using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MeuSiteLogin.Data;
using MeuSiteLogin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

public class UploadsController : Controller
{
    private readonly AppDbContext _context;

    public UploadsController(AppDbContext context)
    {
        _context = context;
    }

    private readonly string[] _tiposPermitidos = { "audio/mpeg", "audio/wav", "video/mp4", "video/quicktime" };

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var arquivos = await _context.Arquivos
            .Include(a => a.Usuario)
            .OrderByDescending(a => a.DataEnvio)
            .ToListAsync();

        ViewBag.Arquivos = arquivos;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(IFormFile arquivo)
    {
        if (arquivo == null || arquivo.Length == 0)
        {
            ModelState.AddModelError("", "Nenhum arquivo selecionado.");
            return RedirectToAction("Index");
        }

        if (!_tiposPermitidos.Contains(arquivo.ContentType))
        {
            ModelState.AddModelError("", "Tipo de arquivo não permitido.");
            return RedirectToAction("Index");
        }

        if (arquivo.Length > 500 * 1024 * 1024) // 500 MB
        {
            ModelState.AddModelError("", "Tamanho máximo excedido (500MB).");
            return RedirectToAction("Index");
        }

        using var memoryStream = new MemoryStream();
        await arquivo.CopyToAsync(memoryStream);

        var novoArquivo = new Arquivo
        {
            NomeOriginal = arquivo.FileName,
            ContentType = arquivo.ContentType,
            Dados = memoryStream.ToArray(),
            UsuarioId = 1 // Substitua por quem estiver logado (ex: via HttpContext)
        };

        _context.Arquivos.Add(novoArquivo);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Download(int id)
    {
        var arquivo = await _context.Arquivos.FindAsync(id);

        if (arquivo == null)
            return NotFound();

        return File(arquivo.Dados, arquivo.ContentType, arquivo.NomeOriginal);
    }

    [HttpPost]
public async Task<IActionResult> Excluir(int id)
{
    // Busca o arquivo pelo id
    var arquivo = await _context.Arquivos.FindAsync(id);
    
    if (arquivo == null)
    {
        // Se não encontrou, talvez mostrar mensagem ou redirecionar
        TempData["Mensagem"] = "Arquivo não encontrado ou já excluído.";
        return RedirectToAction("Index");
    }

    try
    {
        _context.Arquivos.Remove(arquivo);
        await _context.SaveChangesAsync();
        TempData["Mensagem"] = "Arquivo excluído com sucesso!";
    }
    catch (DbUpdateConcurrencyException)
    {
        // Pode ocorrer se outra operação excluiu o arquivo antes
        TempData["Mensagem"] = "Erro ao excluir o arquivo: ele já pode ter sido removido.";
    }
    catch (Exception ex)
    {
        TempData["Mensagem"] = $"Erro inesperado: {ex.Message}";
    }

    return RedirectToAction("Index");
}

    
}
