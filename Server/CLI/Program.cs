using CLI.UI;
using CLI.UI.ManageUsers;
using FileRepositories;

namespace CLI;
using RepositoryContracts;
class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting CLI Appp...");
        IUserRepository userRepository = new UserFileRepository();                          // Old: UserInMemoryRepository();
        ICommentRepository commentRepository = new CommentFileRepository();                        // Old: CommentInMemoryRepository();
        IPostRepository postRepository = new PostFileRepository();                        // Old: PostInMemoryRepository();

        CliApp cliApp = new CliApp(userRepository, commentRepository, postRepository);
        await cliApp.StartAsync();
        
    }
}