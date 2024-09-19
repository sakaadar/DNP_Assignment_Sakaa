using CLI.UI.Utility;
using Entitities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;

    public CreatePostView(IPostRepository postRepository, IUserRepository userRepository)
    {
        this.postRepository = postRepository;
        this.userRepository = userRepository;
        
    }

    public async Task CreatePost()
    {
        Console.WriteLine("Create A New post");
        string  title = string.Empty;
        while (string.IsNullOrEmpty(title))
        {
            Console.WriteLine("Enter a title: ");
            title = Console.ReadLine();
        }
        string body = string.Empty;
        while (string.IsNullOrEmpty(body))
        {
            Console.WriteLine("Enter a body: ");
            body = Console.ReadLine();
        }

        InputValidator inputValidator = new InputValidator(userRepository);
        int userId = await inputValidator.getValidUserId();
        await postRepository.AddAsync(new Post{Title = title,body = body,UserId = userId});
        Console.WriteLine("Post Created");
    }
}