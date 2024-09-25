using System.Text.Json;
using Entitities;
using RepositoryContracts;

namespace FileRepositories;

public class CommentFileRepository: ICommentRepository
{
    private readonly string filePath = "comments.json";

    public CommentFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    public async Task<Comment> AddAsync(Comment comment)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        int maxId = comments.Count > 0 ? comments.Max(c => c.Id) : 1;
        comment.Id = maxId + 1;
        comments.Add(comment);
        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
        return comment;
    }

    public async Task UpdateAsync(Comment comment)
    {
        string commentAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentAsJson)!;
        Comment? existingComment = comments.SingleOrDefault(c => c.Id == comment.Id);
    
        if (existingComment == null)
        {
            throw new InvalidOperationException("Comment could not be found");
        }

        comments.Remove(existingComment);
        comments.Add(comment);

        commentAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentAsJson);
        // return Task.CompletedTask; Hvorfor virker det ikke?
    }


    public async Task DeleteAsync(int id)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        Comment? commentToDelete = comments.SingleOrDefault(c => c.Id == id);
        if (commentToDelete is null)
        {
            throw new InvalidOperationException("Comment could not be found");
        }
        comments.Remove(commentToDelete);
        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
       // return Task.CompletedTask; hvorfor virker det ik??
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
       string commentAsJson = await File.ReadAllTextAsync(filePath);
       List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentAsJson)!;
       Comment? singleComment = comments.SingleOrDefault(c => c.Id == id);
       if (singleComment is null)
       {
           throw new InvalidOperationException("Comment could not be found");
       }

       return singleComment;
    }

    public IQueryable<Comment> GetMany()
    {
        string commentsAsJson = File.ReadAllTextAsync(filePath).Result;
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        return comments.AsQueryable();
    }

    public async Task<List<Comment>> GetCommentsForPostAsync(int postId)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        var commentsForPost = comments.Where(c=>c.PostId==postId).ToList();
        return commentsForPost;
    }
}