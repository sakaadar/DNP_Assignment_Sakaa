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
       while (!string.IsNullOrEmpty(username))
       {
           Console.Write("Username: ");
           username = Console.ReadLine();
           if(await iuserRepository.IsUsernameTakenAsync(username)) //Tjek om brugernavnet allerede er taget
           {
               throw new InvalidOperationException("Username already taken");
           }
       }
       
       await iuserRepository.AddAsync(new User{username=username});
       Console.WriteLine("User Created");
       
    }
}