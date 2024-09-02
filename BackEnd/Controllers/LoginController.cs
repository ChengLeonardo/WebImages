using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using BackEnd.Interface;
using System.Security.Claims;
using BackEnd.Data.Repositorios;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BackEnd.Controllers;

public class LoginController : Controller
{
    private readonly IRepoUsuario _repoUsuario;
    private readonly IRepoRolUsuario _repoRolUsuario;

    public LoginController(IRepoUsuario repoUsuario, IRepoRolUsuario repoRolUsuario)
    {
        _repoUsuario = repoUsuario;
        _repoRolUsuario = repoRolUsuario;
    }
    
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        if(ModelState.IsValid)
        {
            var usuarioExistente = _repoUsuario.SelectWhere(u => u.NombreUsuario == model.NombreUsuario).FirstOrDefault();

            if(usuarioExistente != null)
            {
                if(BCrypt.Net.BCrypt.Verify(model.Contrasena, usuarioExistente.Contrasena))
                {
                    var rol = _repoRolUsuario.IdSelect(usuarioExistente.IdRol);
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuarioExistente.IdUsuario.ToString()),
                        new Claim(ClaimTypes.Name, usuarioExistente.NombreUsuario),
                        new Claim(ClaimTypes.Email, usuarioExistente.Email),
                        new Claim(ClaimTypes.Role, rol.Descripcion)
                    };

                    // Crear la identidad y el principal
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Nombre usuario o contrasena incorrecta.");
        }

        return View(model);
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
