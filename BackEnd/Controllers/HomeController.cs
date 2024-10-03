using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Models;
using BackEnd.Interface;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BackEnd.Controllers;

public class HomeController : Controller
{
    private readonly IWebHostEnvironment _environment;
    private readonly IRepoUsuario _repoUsuario;
    private readonly IRepoRolUsuario _repoRolUsuario;
    public HomeController(IRepoUsuario repoUsuario, IRepoRolUsuario repoRolUsuario, IWebHostEnvironment environment)
    {
        _environment = environment;
        _repoUsuario = repoUsuario;
        _repoRolUsuario = repoRolUsuario;
    }

    [HttpGet]
    public IActionResult Index()
    {
        if (User.Identity.IsAuthenticated)
        {
            return View();
        }
        else
        {
            return RedirectToAction("Login", "Login");
        }
    }
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Login");
    }

    [HttpGet]
    public IActionResult Perfil()
    {
        var usuario = _repoUsuario.IdSelect(Convert.ToUInt16(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
        return View(usuario);
    }

    [HttpPost]
    public async Task<IActionResult> EditarFoto (IFormFile FotoPerfil)
    {
        if(ModelState.IsValid)
        {
            string uniqueFileName = null;

            if(FotoPerfil != null)
            {
                // Obtener el usuario actual de la base de datos
                var usuario = _repoUsuario.IdSelect(Convert.ToUInt16(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value));

                // Obtener la ruta de la foto anterior si existe
                if (!string.IsNullOrEmpty(usuario.FotoPerfil))
                {
                    string previousFilePath = Path.Combine(_environment.WebRootPath, "images", usuario.FotoPerfil);

                    // Comprobar si la foto anterior existe y eliminarla
                    if (System.IO.File.Exists(previousFilePath))
                    {
                        System.IO.File.Delete(previousFilePath);
                    }
                }

                // Guardar la nueva foto
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + FotoPerfil.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await FotoPerfil.CopyToAsync(fileStream); // Uso de CopyToAsync para operaciones asíncronas
                }

                // Actualizar la propiedad FotoPerfil del usuario con el nuevo nombre de archivo
                usuario.FotoPerfil = uniqueFileName;

                // Actualizar el usuario en la base de datos
                _repoUsuario.Update(usuario);
            }
        }
        return RedirectToAction("Perfil", "Home");
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
