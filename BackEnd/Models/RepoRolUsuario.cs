using Microsoft.EntityFrameworkCore;

namespace BackEnd.Models;

public class RepoRolUsuario : RepoBase<RolUsuario, uint>
{
    public RepoRolUsuario(MyDbContext context, DbSet<RolUsuario> dbSet) : base(context, dbSet)
    {
    }
}