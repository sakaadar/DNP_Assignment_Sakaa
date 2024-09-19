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

        int userId = 0;
        bool IsvalidUser = false;
        while (!IsvalidUser)
        {
            Console.WriteLine("Enter a user ID: ");
            var intput = Console.ReadLine();
            if (int.TryParse(intput, out userId))
            {
                var user = await userRepository.GetSingleAsync(userId);
                if (user != null)
                {
                    IsvalidUser = true;
                }
                else
                {
                    Console.WriteLine("Invalid User ID, try again");
                }
            }
            else
            {
                Console.WriteLine("Enter a valid number");
            }
        }
        await postRepository.AddAsync(new Post{Title = title,body = body,UserId = userId});
    }
}