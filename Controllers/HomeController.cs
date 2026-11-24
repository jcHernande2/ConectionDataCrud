using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ConectionDataCrud.Models;

namespace ConectionDataCrud.Controllers;

public class HomeController : Controller
{
    private readonly ConectionDataCrud.Data.AppDbContext _context;

    public HomeController(ConectionDataCrud.Data.AppDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var users = _context.Users.ToList();
            
        return View(users);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
