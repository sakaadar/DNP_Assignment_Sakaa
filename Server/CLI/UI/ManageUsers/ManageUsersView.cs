namespace CLI.UI.ManageUsers;

public class ManageUsersView
{
    private readonly CreateUserView createUserView;
    private readonly ListUsersView listUsersView;
    private readonly DeleteUserView deleteUserView;
    private readonly UpdateUserView updateUserView;

    public ManageUsersView(CreateUserView createUserView,
        ListUsersView listUsersView, DeleteUserView deleteUserView, UpdateUserView updateUserView)
    {
        this.createUserView = createUserView;
        this.listUsersView = listUsersView;
        this.deleteUserView = deleteUserView;
        this.updateUserView = updateUserView;
    }

    public async Task Show()
    {
        Console.WriteLine("Manage Users");
        Console.WriteLine("------------------");
        Console.WriteLine("1. Create User");
        Console.WriteLine("2. List Users");
        Console.WriteLine("3. Delete User");
        Console.WriteLine("4. Update User");
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
            case "3":
                await deleteUserView.deleteuserAsync();
                break;
            case "4":
                await updateUserView.updateUser();
                break;
            default:
                Console.WriteLine("Invalid Choice");
                break;
        }
    }
}