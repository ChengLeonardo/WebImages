using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Models;
using BackEnd.Interface;
using System.Security.Claims;

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
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var mailExistente = _repoUsuario.SelectWhere(u => u.Email == model.usuario.Email).FirstOrDefault();
            if(mailExistente != null)
            {
                ModelState.AddModelError("Email", "El email ya está en uso. Por favor, elige otro.");
                return View(model);
            }
            else if(mailExistente == null)
            {
                var nombreUsuarioExistente = _repoUsuario.SelectWhere(u => u.NombreUsuario == model.usuario.NombreUsuario);
                ModelState.AddModelError("NombreUsuario", "El nombre usuario ya esta en uso. Por favor, elige otro.");
                return View(model);
            }
            else
            {
                var idAutoIncrementado = _repoUsuario.Insert(model.usuario, "IdUsuario");
                model.usuario.IdUsuario = 
                var rol = _repoRolUsuario.IdSelect(model.usuario.IdRol);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, model.usuario.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Name, ),
                    new Claim(ClaimTypes.Email, usuarioExistente.Email),
                    new Claim(ClaimTypes.Role, rol.Descripcion)
                };

                // Crear la identidad y el principal
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                    
                return 
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
