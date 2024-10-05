using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Models;
using BackEnd.Interface;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;


namespace BackEnd.Controllers;

public class HomeController : Controller
{
    private readonly IWebHostEnvironment _environment;
    private readonly IRepoUsuario _repoUsuario;
    private readonly IRepoRolUsuario _repoRolUsuario;
    private readonly IRepoPost _repoPost;
    public HomeController(IRepoUsuario repoUsuario, IRepoRolUsuario repoRolUsuario, IWebHostEnvironment environment, IRepoPost repoPost)
    {
        _environment = environment;
        _repoUsuario = repoUsuario;
        _repoRolUsuario = repoRolUsuario;
        _repoPost = repoPost;
    }

    [HttpGet]
    public IActionResult Index()
    {
        if (User.Identity.IsAuthenticated)
        {
            var allPosts = _repoPost.Select().ToList();
            var viewModel = new IndexViewModel
            {
                AllPosts = allPosts
            };
            return View(viewModel);
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
        var userId = Convert.ToUInt16(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var usuario = _repoUsuario.IdSelect(userId);
        var postsUsuario = _repoPost.SelectWhere(p => p.IdUsuario == userId).ToList();

        var viewModel = new PerfilViewModel
        {
            Usuario = usuario,
            PostsUsuario = postsUsuario
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> EditarFoto(IFormFile NuevaFotoPerfil)
    {
        if(ModelState.IsValid)
        {   
            
            string uniqueFileName = null;

            if(NuevaFotoPerfil != null)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "images");

                uniqueFileName = Guid.NewGuid().ToString() + "_" + NuevaFotoPerfil.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                NuevaFotoPerfil.CopyTo(new FileStream(filePath, FileMode.Create));
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

    [HttpGet]
    public IActionResult Postear()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Postear(PostearViewModel model)
    {
        if (ModelState.IsValid)
        {
            var userId = Convert.ToUInt16(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            string uniqueFileName = null;

            if (model.Imagen != null)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Imagen.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Imagen.CopyToAsync(fileStream);
                }
            }

            var post = new Post
            {
                UrlImagen = uniqueFileName,
                IdUsuario = userId,
                FechaPublicacion = DateTime.Now
            };

            _repoPost.Insert(post, "IdPost"	);

            return RedirectToAction("Perfil");
        }

        return View(model);
    }
}
