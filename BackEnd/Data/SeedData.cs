using BackEnd.Interface;
using BackEnd.Models;

namespace BackEnd.Data;

public class SeedData
{
    public static void Initialize(IRepoUsuario repoUsuario, IRepoRolUsuario repoRolUsuario)
    {
        // Verificar si ya existen datos en la base de datos
        if (!repoUsuario.Select().Any())
        {
            // Agregar datos seed
            var rolAdministrador = new RolUsuario { Descripcion = "Admin" };
            repoRolUsuario.Insert(rolAdministrador, "IdRol");

            var usuarioAdministrador = new Usuario
            {
                NombreUsuario = "admin",
                Contrasena = BCrypt.Net.BCrypt.HashPassword("admin"), // Asegúrate de que la contraseña esté correctamente hasheada
                IdRol = rolAdministrador.IdRol,
                Nombre = "Leonardo",
                Apellido = "Cheng",
                Email = "leonardo.chenget12de1@gmail.com",
                FotoPerfil = null
            };

            repoUsuario.Insert(usuarioAdministrador, "IdUsuario");

                // Agregar más datos seed según sea necesario
        }
    }
}
