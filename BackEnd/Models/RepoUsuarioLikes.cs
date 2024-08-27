using Microsoft.EntityFrameworkCore;

namespace BackEnd.Models;

public class RepoUsuarioLikes : RepoBase<UsuarioLikes, uint>
{
    public RepoUsuarioLikes(MyDbContext context) : base(context, context.Set<UsuarioLikes>())
    {
    }
}