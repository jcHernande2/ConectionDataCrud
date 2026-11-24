using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ConectionDataCrud.Models;
using ConectionDataCrud.Data;
using Microsoft.EntityFrameworkCore;
namespace ConectionDataCrud.Controllers;

public class UsersController : Controller
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

       // LISTAR USUARIOS
    public async Task<IActionResult> Index()
    {
        var users = await _context.Users
            .AsNoTracking()
            .ToListAsync();

        return View(users);
    }

    // MOSTRAR FORMULARIO
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(CreateUserViewModel model)
{
    if (!ModelState.IsValid)
        return View(model);

    // Validar que el correo no esté registrado
    bool emailExists = await _context.Users
        .AnyAsync(u => u.Email == model.Email);

    if (emailExists)
    {
        ModelState.AddModelError(
            nameof(model.Email),
            "Este correo ya está registrado."
        );

        return View(model);
    }

    var user = new Users
    {
        Nombre = model.Nombre,
        Email = model.Email,
        Activo = model.Activo
    };

    // Generar hash seguro de la contraseña
    /*var passwordHasher = new PasswordHasher<Users>();

    user.PasswordHash = passwordHasher.HashPassword(
        user,
        model.Password
    );*/

    _context.Users.Add(user);

    await _context.SaveChangesAsync();

    return RedirectToAction(nameof(Index));
}
}