namespace CLI.UI.ManageComments;

public class ManageCommentsView
{
    private readonly CreateCommentView createCommentView;
    private readonly ListCommentsForPostView commentsForPostView;

    public ManageCommentsView(CreateCommentView createCommentView,
        ListCommentsForPostView commentsForPostView)
    {
        this.createCommentView = createCommentView;
        this.commentsForPostView = commentsForPostView;
    }

    public async Task showComments()
    {
        Console.WriteLine("Manage Comments");
        Console.WriteLine("-----------------");
        Console.WriteLine("1. Create Comment for Post");
        Console.WriteLine("2. List Comments for Post");
        Console.WriteLine("Choose option: ");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                await createCommentView.createComment();
                break;
            case "2":
                await commentsForPostView.ListCommentsForPost();
                break;
            default:
                Console.WriteLine("Invalid option");
                break;
        }
    }
}