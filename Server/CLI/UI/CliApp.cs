using CLI.UI.ManageComments;
using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    private readonly IUserRepository iuserRepository;
    private readonly ICommentRepository icommentRepository;
    private readonly IPostRepository ipostRepository;
    
    public CliApp(IUserRepository iuserRepository, ICommentRepository icommentRepository, IPostRepository ipostRepository)
    {
        this.iuserRepository = iuserRepository;
        this.icommentRepository = icommentRepository;
        this.ipostRepository = ipostRepository;
    }

    public async Task StartAsync()
    {
        while (true)
        {
            Console.WriteLine("Menu: ");
            Console.WriteLine("1. Manage Users");
            Console.WriteLine("2. Manage Posts");
            Console.WriteLine("3. Manage Comments");
            Console.WriteLine("0. Exit");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "0":
                    return;
                case "1":
                    await ManageUsers();
                    break;
                case "2":
                   await ManagePosts();
                   break;
                case "3":
                    await ManageComments();
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again");
                    break;
            }
        }
    }

    private async Task ManageUsers()
    {
        CreateUserView createUserView = new CreateUserView(iuserRepository);
        ListUsersView listUsersView = new ListUsersView(iuserRepository);
        DeleteUserView deleteUserView = new DeleteUserView(iuserRepository);
        UpdateUserView updateUserView = new UpdateUserView(iuserRepository);
        ManageUsersView manageUsersView = new ManageUsersView(createUserView, listUsersView, deleteUserView, updateUserView);
        await manageUsersView.Show();
    }

    private async Task ManagePosts()
    {
        CreatePostView createPostView = new CreatePostView(ipostRepository, iuserRepository);
        ListsPostView listsPostView = new ListsPostView(ipostRepository);
        SinglePostView singlePostView = new SinglePostView(ipostRepository);
        ManagePostsView managePostsView = new ManagePostsView(createPostView, listsPostView, singlePostView);
        await managePostsView.ShowPosts();
    }

    public async Task ManageComments()
    {
        CreateCommentView createCommentView = new CreateCommentView(icommentRepository, iuserRepository);
        ListCommentsForPostView listCommentsForPostView = new ListCommentsForPostView(icommentRepository);
        ManageCommentsView manageCommentsView = new ManageCommentsView(createCommentView, listCommentsForPostView);
        await manageCommentsView.showComments();
    }
}