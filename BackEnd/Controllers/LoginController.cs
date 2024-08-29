using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BackEnd.Controllers;

public class LoginController : Controller
{
    private readonly RepoUsuario _repoUsuario;

    public LoginController(RepoUsuario repoUsuario)
    {
        _repoUsuario = repoUsuario;
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Login(Usuario usuario)
    {
        if(ModelState.IsValid)
        {
            var existe = _repoUsuario.SelectWhere(u => u.NombreUsuario == usuario.NombreUsuario && u.Contrasena == usuario.Contrasena).FirstOrDefault();

            if(existe != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return
        }
        return View(usuario);

    }

    public IActionResult Register()
    {
    // Lógica para manejar el registro
    // Luego redirige a la página deseada
        return RedirectToAction("Index", "Register");
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
