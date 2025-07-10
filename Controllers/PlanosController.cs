using Microsoft.AspNetCore.Mvc;
using MeuSiteLogin.Data;
using MeuSiteLogin.Models;

public class PlanosController : Controller
{
    private readonly AppDbContext _context;

    public PlanosController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
}
