using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class ListCommentsForPostView
{
    private readonly ICommentRepository commentRepository;

    public ListCommentsForPostView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task ListCommentsForPost()
    {
        Console.WriteLine("Listing comments for post");
        Console.WriteLine("Enter a post ID: ");
        if (int.TryParse(Console.ReadLine(), out int postId))
        {
            var comments =
                await commentRepository.GetCommentsForPostAsync(postId);
            if (comments.Any())
            {
                foreach (var comment in comments)
                {
                    Console.WriteLine(comment.ToString());
                }
            }
            else
            {
                Console.WriteLine("No comments found..");
            }
        }
        else
        {
            Console.WriteLine("Enter a Valid post ID ");
        }
    }
}