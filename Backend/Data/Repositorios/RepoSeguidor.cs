using BackEnd.Interface;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data.Repositorios;

public class RepoSeguidor : RepoBase<Seguidor, uint>, IRepoSeguidor
{
    public RepoSeguidor(MyDbContext context) : base(context, context.Set<Seguidor>())
    {
    }
}