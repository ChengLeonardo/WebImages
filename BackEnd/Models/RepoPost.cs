using Microsoft.EntityFrameworkCore;

namespace BackEnd.Models;

public class RepoPost : RepoBase<Post, uint>
{
    public RepoPost(MyDbContext context) : base(context, context.Set<Post>())
    {
    }
}