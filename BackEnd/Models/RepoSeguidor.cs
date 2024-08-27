using Microsoft.EntityFrameworkCore;

namespace BackEnd.Models;

public class RepoSeguidor : RepoBase<Seguidor, uint>
{
    public RepoSeguidor(MyDbContext context) : base(context, context.Set<Seguidor>())
    {
    }
}