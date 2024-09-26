using System.Text.Json;
using Entitities;
using RepositoryContracts;

namespace FileRepositories;

public class PostFileRepository : IPostRepository
{
    private readonly string filePath = "posts.json";

    public PostFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public async Task<Post> AddAsync(Post post)
    {
        string postsAsjson = await File.ReadAllTextAsync(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsjson)!;
        int maxId = posts.Count > 0 ? posts.Max(p => p.Id) : 1;
        post.Id = maxId + 1;
        posts.Add(post);
        postsAsjson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postsAsjson);
        return post;
    }

    public async Task UpdateAsync(Post post)
    {
       string postsAsjson = await File.ReadAllTextAsync(filePath);
       List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsjson)!;
       Post? existingPost = posts.SingleOrDefault(p => p.Id == post.Id);
       if (existingPost == null)
       {
           throw new ArgumentException($"Post {post.Id} was not found");
       }
       posts.Remove(existingPost);
       posts.Add(post);
       postsAsjson = JsonSerializer.Serialize(posts);
       await File.WriteAllTextAsync(filePath, postsAsjson);
    }

    public async Task DeleteAsync(int id)
    {
       string postsAsjson = await File.ReadAllTextAsync(filePath);
       List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsjson)!;
       Post? postToDelete = posts.SingleOrDefault(p => p.Id == id);
       if (postToDelete is null)
       {
           throw new ArgumentException($"Post {id} was not found");
       }
       posts.Remove(postToDelete);
       postsAsjson = JsonSerializer.Serialize(posts);
       await File.WriteAllTextAsync(filePath, postsAsjson);
    }

    public async Task<Post> GetSingleAsync(int id)
    {
       string postsAsjson = await File.ReadAllTextAsync(filePath);
       List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsjson)!;
       Post? singlePost = posts.SingleOrDefault(p => p.Id == id);
       if (singlePost is null)
       {
           throw new ArgumentException($"Post {id} does not exits");
       }
       return singlePost;
    }

    public IQueryable<Post> GetMany()
    {
        string postsAsjson = File.ReadAllTextAsync(filePath).Result;
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsjson)!;
        return posts.AsQueryable();
    }
}