using CLI.UI;
using CLI.UI.ManageUsers;

namespace CLI;
using InMemoryRepositories;
using RepositoryContracts;
class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting CLI Appp...");
        IUserRepository userRepository = new UserInMemoryRepository();
        ICommentRepository commentRepository = new CommentInMemoryRepository();
        IPostRepository postRepository = new PostInMemoryRepository();

        CliApp cliApp = new CliApp(userRepository, commentRepository, postRepository);
        await cliApp.StartAsync();
        
    }
}