using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Models;
using BackEnd.Interface;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers;

public class HomeController : Controller
{
    private readonly IWebHostEnvironment _environment;
    private readonly IRepoUsuario _repoUsuario;
    private readonly IRepoRolUsuario _repoRolUsuario;
    private readonly IRepoPost _repoPost;
    private readonly IRepoUsuarioLikes _repoUsuarioLikes;
    public HomeController(IRepoUsuario repoUsuario, IRepoRolUsuario repoRolUsuario, IWebHostEnvironment environment, IRepoPost repoPost, IRepoUsuarioLikes repoUsuarioLikes)
    {
        _environment = environment;
        _repoUsuario = repoUsuario;
        _repoRolUsuario = repoRolUsuario;
        _repoPost = repoPost;
        _repoUsuarioLikes = repoUsuarioLikes;
    }

    [HttpGet]
    public IActionResult Index()
    {
        if (User.Identity.IsAuthenticated)
        {
            List<Post> allPosts = _repoPost.Select().Include(p => p.Usuario).Include(p => p.ListLikes).ToList();
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
    public IActionResult Perfil(uint? id)
    {
        uint IdUsuario;
        if(id == null)
        {
            IdUsuario = Convert.ToUInt16(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
        else
        {
            IdUsuario = id.Value;
        }
        var usuario = _repoUsuario.IdSelect(IdUsuario);
        var postsUsuario = _repoPost.SelectWhere(p => p.IdUsuario == IdUsuario).Include(p => p.ListLikes).ToList();

        var viewModel = new PerfilViewModel
        {
            EsUsuarioActual = (id == null),
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

    [HttpGet]
    public IActionResult DetallePost(uint id)
    {
        var post = _repoPost.IdSelect(id);
        var usuario = _repoUsuario.IdSelect(post.IdUsuario);
        var viewModel = new DetallePostViewModel { Post = post, Usuario = usuario };
        return View(viewModel);
    }
    

    [HttpPost]
    public IActionResult LikePost(uint id, string returnUrl)
    {
        var userId = Convert.ToUInt16(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var post = _repoPost.IdSelect(id);

        // Verificar si el usuario que realiza la acción es el mismo que creó el post
        if (post.IdUsuario == userId)
        {
            // Si es el mismo usuario, no permitir la acción
            return Redirect(returnUrl);
        }

        var like = _repoUsuarioLikes.SelectWhere(l => l.IdUsuario == userId && l.IdPost == id).FirstOrDefault();

        if (like == null)
        {
            // El usuario no ha dado like, así que lo agregamos
            var nuevoLike = new UsuarioLikes
            {
                IdUsuario = userId,
                IdPost = id
            };
            post.ListLikes.Add(nuevoLike);
            _repoPost.Update(post);
        }
        else
        {
            // El usuario ya dio like, así que lo quitamos
            _repoUsuarioLikes.Delete(like);
            post.ListLikes.Remove(like);
        }

        _repoPost.Update(post);
        
        // Usar la URL de retorno proporcionada
        return Redirect(returnUrl);
    }

}
