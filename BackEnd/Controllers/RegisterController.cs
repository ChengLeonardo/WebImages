using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Models;
using BackEnd.Interface;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BackEnd.Controllers;

public class RegisterController : Controller
{
    private readonly IRepoUsuario _repoUsuario;
    private readonly IRepoRolUsuario _repoRolUsuario;
    public RegisterController(IRepoUsuario repoUsuario, IRepoRolUsuario repoRolUsuario)
    {
        _repoUsuario = repoUsuario;
        _repoRolUsuario = repoRolUsuario;
    }

    [HttpGet]
    public IActionResult Register_1()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register_1(Register_1ViewModel model)
    {
        if(ModelState.IsValid)
        {
            var nombreUsuarioExistente = _repoUsuario.SelectWhere(u => u.NombreUsuario == model.NombreUsuario);
            if(nombreUsuarioExistente != null)
            {
                ModelState.AddModelError("NombreUsuario", "El nombre usuario ya esta en uso. Por favor, elige otro.");
                return View(model);
            }
            else
            {
                TempData["Nombre"] = model.Nombre;
                TempData["Apellido"] = model.Apellido;
                TempData["NombreUsuario"] = model.NombreUsuario;

                return RedirectToAction("Register_2");
            }
        }
        else
        {
            return View(model);
        }
    }

    [HttpGet]
    public IActionResult Register_2()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register_2(Register_2ViewModel model)
    {
        if (ModelState.IsValid)
        {
            var mailExistente = _repoUsuario.SelectWhere(u => u.Email == model.Email).FirstOrDefault();
            if(mailExistente != null)
            {
                ModelState.AddModelError("Email", "El email ya está en uso. Por favor, elige otro.");
                return View(model);
            }
            else
            {                
                var rol = _repoRolUsuario.IdSelect(1);

                var usuario = new Usuario
                {
                    Email = model.Email,
                    Contrasena = model.Contrasena,
                    Apellido = TempData["Apellido"].ToString(),
                    Nombre = TempData["Nombre"].ToString(),
                    NombreUsuario = TempData["NombreUsuario"].ToString(),
                    FotoPerfil = null,
                    IdRol =  1,
                    IdUsuario = 0,
                    Rol = rol
                };

                var idAutoIncrementado = _repoUsuario.Insert(usuario, "IdUsuario");
                usuario.IdUsuario = idAutoIncrementado;

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Name, usuario.NombreUsuario.ToString()),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Role, rol.Descripcion)
                };

                // Crear la identidad y el principal
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                    
                return RedirectToAction("Index", "Home");
            }
        }
        else
        {
            return View(model);
        }
        
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult Login()
    {
        return RedirectToAction("Index", "Login");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
