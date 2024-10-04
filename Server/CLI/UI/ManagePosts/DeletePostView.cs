using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class DeletePostView
{
    private readonly IPostRepository ipostRepository;

    public DeletePostView(IPostRepository ipostRepository)
    {
        this.ipostRepository = ipostRepository;
    }

    public async Task DeletePostAsync()
    {
        Console.WriteLine("Enter a post ID: ");
        string? input = Console.ReadLine();
        if (int.TryParse(input, out int postId))
        {
            await ipostRepository.DeleteAsync(postId);
            Console.WriteLine("Post deleted Successfully!");
        }
        else
        {
            Console.WriteLine("Invalid post ID!, please try again!");
        }
    }
}