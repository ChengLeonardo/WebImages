using Microsoft.EntityFrameworkCore;

namespace BackEnd.Models;

public class RepoUsuarioLikes : RepoBase<UsuarioLikes, uint>
{
    public RepoUsuarioLikes(MyDbContext context, DbSet<UsuarioLikes> dbSet) : base(context, dbSet)
    {
    }
}