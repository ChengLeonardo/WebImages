using System.Data;

using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data;

public class MyDbContext : DbContext
{
    public DbSet<Usuario> Usuario { get; set; }
    public DbSet<Post> Post { get; set; }
    public DbSet<UsuarioLikes> UsuarioLikes { get; set; }
    public DbSet<Seguidor> Seguidor { get; set; }
    public DbSet<RolUsuario> RolUsuario { get; set; }
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Post>()
            .HasOne(p => p.Usuario)
            .WithMany(u => u.Posteos)
            .HasForeignKey(p => p.IdUsuario);

        // Configuración de RolUsuario
        modelBuilder.Entity<RolUsuario>()
            .HasKey(r => r.IdRol);

        // Configuración de Usuario
        modelBuilder.Entity<Usuario>()
            .HasKey(u => u.IdUsuario);

        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Rol)
            .WithMany() // Suponiendo que no hay navegación inversa
            .HasForeignKey(u => u.IdRol);

        // Configuración de Post
        modelBuilder.Entity<Post>()
            .HasKey(p => p.IdPost);

        modelBuilder.Entity<Post>()
            .HasOne(p => p.Usuario)
            .WithMany() // Suponiendo que no hay navegación inversa
            .HasForeignKey(p => p.IdUsuario);

        // Configuración de UsuarioLikes
        modelBuilder.Entity<UsuarioLikes>()
            .HasKey(ul => new { ul.IdUsuario, ul.IdPost });

        modelBuilder.Entity<UsuarioLikes>()
            .HasOne(ul => ul.Usuario)
            .WithMany() // Suponiendo que no hay navegación inversa
            .HasForeignKey(ul => ul.IdUsuario);

        modelBuilder.Entity<UsuarioLikes>()
            .HasOne(ul => ul.Post)
            .WithMany() // Suponiendo que no hay navegación inversa
            .HasForeignKey(ul => ul.IdPost);

        // Configuración de Seguidor
        modelBuilder.Entity<Seguidor>()
            .HasKey(s => new { s.IdUsuario, s.IdUsuarioSeguido });

        modelBuilder.Entity<Seguidor>()
            .HasOne(s => s.Usuario)
            .WithMany() // Suponiendo que no hay navegación inversa
            .HasForeignKey(s => s.IdUsuario);

        modelBuilder.Entity<Seguidor>()
            .HasOne(s => s.UsuarioSeguido)
            .WithMany() // Suponiendo que no hay navegación inversa
            .HasForeignKey(s => s.IdUsuarioSeguido);
    }
}