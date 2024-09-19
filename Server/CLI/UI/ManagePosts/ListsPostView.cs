using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ListsPostView
{
    private readonly IPostRepository postRepository;

    public ListsPostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task ListPostsAsync()
    {
        Console.WriteLine("Listing all posts..");
        var posts = postRepository.GetMany();
        if (!posts.Any())
        {
            Console.WriteLine("There are no posts yet");           
        }
        foreach (var post in posts)
        {
            Console.WriteLine(post.ToString());
        }
    }
}