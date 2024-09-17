namespace CLI;
using InMemoryRepositories;
using RepositoryContracts;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting CLI Appp...");
        IUserRepository userRepository = new UserInMemoryRepository();
        ICommentRepository commentRepository = new CommentInMemoryRepository();
        IPostRepository postRepository = new PostInMemoryRepository();
        
        
    }
}