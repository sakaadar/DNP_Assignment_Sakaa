using CLI.UI.Utility;
using Entitities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class CreateCommentView
{
    private readonly ICommentRepository commentRepository;
    private readonly IUserRepository userRepository;

    public CreateCommentView(ICommentRepository commentRepository, IUserRepository userRepository)
    {
        this.commentRepository = commentRepository;
        this.userRepository = userRepository;
    }

    public async Task createComment()
    {
        Console.WriteLine("Create A New Comment");
        string body = string.Empty;
        while (string.IsNullOrEmpty(body))
        {
            Console.WriteLine("Enter Body");
            body = Console.ReadLine();
        }

        InputValidator inputValidator = new InputValidator(userRepository);
        int userId = await inputValidator.getValidUserId();

        await commentRepository.AddAsync(new Comment
            { body = body, UserId = userId });   
    }
}