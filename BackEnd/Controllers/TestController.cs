using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Models;
using System.CodeDom.Compiler;

namespace BackEnd.Controllers;

public class TestController : Controller
{
    private readonly RepoRolUsuario _repoRolUsuario;

    public TestController(RepoRolUsuario repoRolUsuario)
    {
        _repoRolUsuario = repoRolUsuario;
    }

    public IActionResult Index()
    {
        List<RolUsuario> rolUsuarios = _repoRolUsuario.Select();
        return View(rolUsuarios);
    }
    [HttpGet]
    public IActionResult Insert()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Insert(RolUsuario rolUsuario)
    {
        _repoRolUsuario.Insert(rolUsuario, "IdRol");
        return View();
    }
    [HttpGet]
    public IActionResult Delete()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Delete(RolUsuario rolUsuario)
    {
        _repoRolUsuario.Delete(rolUsuario);
        return View();
    }
    [HttpGet]
    public IActionResult Update()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Update(RolUsuario rolUsuario)
    {
        _repoRolUsuario.Update(rolUsuario);
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
}
