using Microsoft.EntityFrameworkCore;

namespace BackEnd.Models;

public class RepoRolUsuario : RepoBase<RolUsuario, uint>
{
    public RepoRolUsuario(MyDbContext context) : base(context, context.Set<RolUsuario>())
    {
    }
}