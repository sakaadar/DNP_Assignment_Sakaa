using Entitities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class CreateUserView
{
    private readonly IUserRepository iuserRepository;
    public CreateUserView(IUserRepository iuserRepository)
    {
       this.iuserRepository = iuserRepository;
    }

    public async Task Create()
    {
       Console.WriteLine("Create A New User--");
       string username = string.Empty;
       while (string.IsNullOrEmpty(username))
       {
           Console.Write("Username: ");
           username = Console.ReadLine();
       }
       //if(username)
    }
}