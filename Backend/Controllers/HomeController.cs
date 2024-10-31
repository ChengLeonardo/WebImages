using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Models;
using BackEnd.Interface;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
    public IActionResult Index(List<Post> allPosts)
    {
        if (User.Identity.IsAuthenticated)
        {
            // Recuperar AllPosts de TempData si está disponible
            if (TempData["AllPosts"] != null)
            {
                allPosts = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Post>>(TempData["AllPosts"].ToString());
            }
            else if (allPosts == null || allPosts.Count == 0)
            {
                allPosts = _repoPost.Select().Include(p => p.Usuario).Include(p => p.ListLikes).ToList();
                Console.WriteLine("No hay posts disponibles para mostrar, se cargan desde la base de datos.");
            }

            var viewModel = new IndexViewModel
            {
                AllPosts = allPosts.OrderBy(p => Guid.NewGuid()).ToList()
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

                // Eliminar la foto existente si hay una
                var usuario = _repoUsuario.IdSelect(Convert.ToUInt16(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
                if(usuario.FotoPerfil != null)
                {
                    string filePathExistente = Path.Combine(uploadsFolder, usuario.FotoPerfil);
                    if(System.IO.File.Exists(filePathExistente))
                    {
                        System.IO.File.Delete(filePathExistente);
                    }
                }

                uniqueFileName = Guid.NewGuid().ToString() + "_" + NuevaFotoPerfil.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                NuevaFotoPerfil.CopyTo(new FileStream(filePath, FileMode.Create));
                usuario.FotoPerfil = uniqueFileName;
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

    [HttpGet]
    public IActionResult CrearPost()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CrearPost(PostearViewModel model)
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
                FechaPublicacion = DateTime.Now,
                Titulo = model.Titulo,
                Contenido = model.Descripcion
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


        [HttpPost]
        public async Task<IActionResult> Editar(UsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var id = Convert.ToUInt16(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var usuario =  _repoUsuario.IdSelect(id);

                switch (model.Que)
                {
                    case "Correo":
                        usuario.Email = model.Email;
                        break;
                    case "Nombre":
                        usuario.Nombre = model.Nombre;
                        break;
                    case "Apellido":
                        usuario.Apellido = model.Apellido;
                        break;
                    case "NombreUsuario":
                        usuario.NombreUsuario = model.NombreUsuario;
                        break;
                }
                // Aquí puedes agregar la lógica para actualizar la foto de perfil si se proporciona
                _repoUsuario.Update(usuario);
                Console.WriteLine("listo");
            }
            return RedirectToAction("Perfil", "Home");
        }

        [HttpGet]
        public IActionResult Configuracion()
        {
            var id = Convert.ToUInt16(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var usuario = _repoUsuario.IdSelect(id); // Método para obtener el usuario actual
            var model = new UsuarioViewModel
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                NombreUsuario = usuario.NombreUsuario
            };
            return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CambiarPost(IndexViewModel model)
    {
        if (model.AllPosts == null || model.AllPosts.Count == 0)
        {
            Console.WriteLine("No hay posts disponibles para cambiar.");
            ModelState.AddModelError("", "No hay posts disponibles para cambiar.");
            return RedirectToAction("Index", "Home");
        }

        if (model.Direction == "up")
        {
            var primerElemento = model.AllPosts[0];
            model.AllPosts.RemoveAt(0);
            model.AllPosts.Add(primerElemento);
        }
        else if (model.Direction == "down")
        {
            var ultimoElemento = model.AllPosts[model.AllPosts.Count - 1];
            model.AllPosts.RemoveAt(model.AllPosts.Count - 1);
            model.AllPosts.Insert(0, ultimoElemento);
        }

        // Almacenar el modelo actualizado en TempData
        TempData["AllPosts"] = JsonConvert.SerializeObject(model.AllPosts);

        // Redirigir a Index
        return RedirectToAction(nameof(Index), "Home");
    }
}
