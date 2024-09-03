using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Models;
using BackEnd.Interface;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BackEnd.Controllers;

public class HomeController : Controller
{
    private readonly IRepoUsuario _repoUsuario;
    private readonly IRepoRolUsuario _repoRolUsuario;
    public HomeController(IRepoUsuario repoUsuario, IRepoRolUsuario repoRolUsuario)
    {
        _repoUsuario = repoUsuario;
        _repoRolUsuario = repoRolUsuario;
    }

    [HttpGet]
    public IActionResult Index()
    {
     
        return View();
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
