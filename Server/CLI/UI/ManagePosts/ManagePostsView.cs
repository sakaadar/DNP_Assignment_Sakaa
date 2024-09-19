using CLI.UI.ManageUsers;

namespace CLI.UI.ManagePosts;

public class ManagePostsView
{
    private readonly CreatePostView postView;
    private readonly ListsPostView listsView;
    private readonly SinglePostView singlePostView;

    public ManagePostsView(CreatePostView postView,
        ListsPostView listsView, SinglePostView singlePostView)
    { 
        this.postView = postView;
        this.listsView = listsView;
        this.singlePostView = singlePostView;
    }

    public async Task ShowPosts()
    {
        Console.WriteLine("Manage Posts");
        Console.WriteLine("--------------");
        Console.WriteLine("1. Create post");
        Console.WriteLine("2. List post");
        Console.WriteLine("3. Single post");
        Console.WriteLine("Choose Option");
        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                await postView.CreatePost();
                break;
            case "2":
                await listsView.ListPostsAsync();
                break;
            case "3":
                await singlePostView.showPost();
                break;
            default:
                Console.WriteLine("Invalid Choice");
                break;
        }
    }
}