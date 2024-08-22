using Microsoft.EntityFrameworkCore;

namespace BackEnd.Models;

public class RepoSeguidor : RepoBase<Seguidor, uint>
{
    public RepoSeguidor(MyDbContext context, DbSet<Seguidor> dbSet) : base(context, dbSet)
    {
    }
}