using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ListUsersView
{
    private readonly IUserRepository userRepository;

    public ListUsersView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task listUsersAsync()
    {
        Console.WriteLine("Listing Users... ");
        var users = userRepository.GetMany();
        if (!users.Any())
        {
            Console.WriteLine("There are no users.");           
        }

        foreach (var user in users)
        {
            Console.WriteLine(user.ToString());
        }
    }
    
}