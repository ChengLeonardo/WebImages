using BackEnd.Interface;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data.Repositorios;

public class RepoPost : RepoBase<Post, uint>, IRepoPost
{
    public RepoPost(MyDbContext context) : base(context, context.Set<Post>())
    {
    }
}