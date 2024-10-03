using BackEnd.Interface;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data.Repositorios;

public class RepoUsuario : RepoBase<Usuario, uint>, IRepoUsuario
{
    public RepoUsuario(MyDbContext context) : base(context, context.Set<Usuario>())
    {
    }
}