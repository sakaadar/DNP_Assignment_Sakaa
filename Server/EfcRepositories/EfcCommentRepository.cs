using Entitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcCommentRepository : ICommentRepository
{
    private readonly AppContext ctx;

    public EfcCommentRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }
    public async Task<Comment> AddAsync(Comment comment)
    {
       EntityEntry<Comment> entry = ctx.Entry(comment);
       await ctx.SaveChangesAsync();
       return entry.Entity;
    }

    public async Task UpdateAsync(Comment comment)
    {
        if (!(await ctx.Comments.AnyAsync(c => c.Id == comment.Id)))
        {
            throw new Exception($"Comment with id {comment.Id} not found");
        }
        ctx.Comments.Update(comment);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
       Comment? existing = ctx.Comments.FirstOrDefault(c => c.Id == id);
       if (existing == null)
       {
           throw new Exception($"Comment with id {id} not found");
       }
       ctx.Comments.Remove(existing);
       await ctx.SaveChangesAsync();
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
      Comment? singleComment = await ctx.Comments.FirstOrDefaultAsync(c => c.Id == id);
      if (singleComment == null)
      {
          throw new Exception($"Comment with id {id} not found");
      }
      return singleComment;
    }

    public IQueryable<Comment> GetMany()
    {
        return ctx.Comments.AsQueryable();
    }

    public async Task<List<Comment>> GetCommentsForPostAsync(int postId)
    {
     return await ctx.Comments.Where(c=>c.PostId == postId).ToListAsync();
    }
}