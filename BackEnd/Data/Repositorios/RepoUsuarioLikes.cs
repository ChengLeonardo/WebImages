using BackEnd.Interface;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data.Repositorios;

public class RepoUsuarioLikes : RepoBase<UsuarioLikes, uint>, IRepoUsuarioLikes
{
    public RepoUsuarioLikes(MyDbContext context) : base(context, context.Set<UsuarioLikes>())
    {
    }
}