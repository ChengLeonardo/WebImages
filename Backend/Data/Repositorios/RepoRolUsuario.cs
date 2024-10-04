using BackEnd.Interface;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data.Repositorios;

public class RepoRolUsuario : RepoBase<RolUsuario, uint>, IRepoRolUsuario
{
    public RepoRolUsuario(MyDbContext context) : base(context, context.Set<RolUsuario>())
    {
    }
}