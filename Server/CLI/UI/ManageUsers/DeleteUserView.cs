using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class DeleteUserView
{
    private readonly IUserRepository iuserRepository;

    public DeleteUserView(IUserRepository iuserRepository)
    {
        this.iuserRepository = iuserRepository;
    }

    public async Task deleteuserAsync()
    {
        Console.WriteLine("Please Enter a user id: ");
        string input = Console.ReadLine();
        if (int.TryParse(input, out int userId))
        {
            await iuserRepository.DeleteAsync(userId);
            Console.WriteLine("User deleted successfully!");
        }
        else
        {
            Console.WriteLine("Invalid user id, please Enter a valid user id: .");
        }
    }
    
}