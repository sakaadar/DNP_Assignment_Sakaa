using Entitities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository: ICommentRepository
{
    private List<Comment> comments;

    public CommentInMemoryRepository()
    {
        comments = new List<Comment>();
    }
    public Task<Comment> AddAsync(Comment comment)
    {
        comment.Id = comments.Any() ? comments.Max(p => p.Id) + 1 : 1;
        comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task UpdateAsync(Comment comment)
    {
        Comment? existingComment = comments.FirstOrDefault(c => c.Id == comment.Id);
        if (existingComment is null)
        {
            throw new InvalidOperationException($"Comment with id {comment.Id} is not found");
        }
        comments.Remove(existingComment);
        comments.Add(comment);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Comment? commentToDelete = comments.FirstOrDefault(c => c.Id == id);
        if (commentToDelete is null)
        {
            throw new InvalidOperationException($"Comment with id {id} is not found");
        }
        comments.Remove(commentToDelete);
        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleAsync(int id)
    {
        Comment? SingleComment = comments.FirstOrDefault(c => c.Id == id);
        if (SingleComment is null)
        {
            throw new InvalidOperationException($"Comment with id {id} is not found");
        }
        return Task.FromResult(SingleComment);
    }

    public IQueryable<Comment> GetMany()
    {
        return comments.AsQueryable();
    }

    public Task<List<Comment>> GetCommentsForPostAsync(int postId)
    {
        var commentsForPost = comments.Where(c=>c.PostId==postId).ToList(); //Filtrere listen og henter kun dem som har samme PostId som det angivne fra brugeren. 
        return Task.FromResult(commentsForPost);
    }
}