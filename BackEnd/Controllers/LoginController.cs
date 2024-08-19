using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Models;

namespace BackEnd.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;

    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
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

    [HttpPost]
    public IActionResult Register()
    {
    // Lógica para manejar el registro
    // Luego redirige a la página deseada
        return RedirectToAction("Index", "RegisterController");
    }

}
