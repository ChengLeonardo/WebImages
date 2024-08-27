using Microsoft.EntityFrameworkCore;

namespace BackEnd.Models;

public class RepoUsuario : RepoBase<Usuario, uint>
{
    public RepoUsuario(MyDbContext context) : base(context, context.Set<Usuario>())
    {
    }
}