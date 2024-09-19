using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository postRepository;

    public SinglePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task showPost()
    {
        Console.WriteLine("Showing single post");
        Console.WriteLine("Enter post id: ");
        if (int.TryParse(Console.ReadLine(),out int postId))
        {
            var post = await postRepository.GetSingleAsync(postId);
            if (post != null)
            {
                Console.WriteLine($"Post Id: {post.Id}");
                Console.WriteLine($"Title: {post.Title}");
                Console.WriteLine($"Body: {post.body}");
                Console.WriteLine($"User Id: {post.UserId}");
            }
            else
            {
                Console.WriteLine("Could not find post");
            }
        }
        else
        {
            Console.WriteLine("Enter a valid number");
        }
    }
}