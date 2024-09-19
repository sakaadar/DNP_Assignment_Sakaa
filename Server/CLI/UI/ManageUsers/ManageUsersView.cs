namespace CLI.UI.ManageUsers;

public class ManageUsersView
{
    private readonly CreateUserView createUserView;
    private readonly ListUsersView listUsersView;

    public ManageUsersView(CreateUserView createUserView,
        ListUsersView listUsersView)
    {
        this.createUserView = createUserView;
        this.listUsersView = listUsersView;
    }

    public async Task Show()
    {
        Console.WriteLine("Manage Users");
        Console.WriteLine("------------------");
        Console.WriteLine("1. Create User");
        Console.WriteLine("2. List Users");
        Console.WriteLine("Choose option");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                await createUserView.Create();
                break;
            case "2":
                await listUsersView.listUsersAsync();
                break;
            default:
                Console.WriteLine("Invalid Choice");
                break;
        }
    }
}