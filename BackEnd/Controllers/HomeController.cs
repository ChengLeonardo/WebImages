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
    public async Task<IActionResult> EditarFoto(IFormFile FotoPerfil)
    {
        if(ModelState.IsValid)
        {   
            string uniqueFileName = null;

            if(FotoPerfil != null)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "images");

                uniqueFileName = Guid.NewGuid().ToString() + "_" + FotoPerfil.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                FotoPerfil.CopyTo(new FileStream(filePath, FileMode.Create));
            }

            var usuario = _repoUsuario.IdSelect(Convert.ToUInt16(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
            usuario.FotoPerfil = uniqueFileName;

            _repoUsuario.Update(usuario);

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
