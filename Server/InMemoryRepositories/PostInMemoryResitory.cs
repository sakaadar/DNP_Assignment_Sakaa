using Entitities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepository: IPostRepository
{
    private List<Post> posts;

    public PostInMemoryRepository()
    {
        posts = new List<Post>();
    }
    public Task<Post> AddAsync(Post post)
    {
        post.Id = posts.Any() ? posts.Max(p => p.Id) + 1 : 1;
        posts.Add(post);
        return Task.FromResult(post);
    }

    public Task UpdateAsync(Post post)
    {
        Post? existingPost = posts.SingleOrDefault(p=>p.Id==post.Id);
        if (existingPost is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{post.Id}' does not exist");
        }
        posts.Remove(existingPost);
        posts.Add(post);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Post? postToRemove = posts.SingleOrDefault(p => p.Id == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' does not exist");
        }

        posts.Remove(postToRemove);
        return Task.CompletedTask;
    }

    public Task<Post> GetSingleAsync(int id)
    {
        Post? SinglePost = posts.SingleOrDefault(p => p.Id == id);
        if (SinglePost is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' does not exist");
        }
        return Task.FromResult(SinglePost);
    }

    public IQueryable<Post> GetMany()
    {
        return posts.AsQueryable();
    }
}