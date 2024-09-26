using Entitities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class UpdateUserView
{
    private readonly IUserRepository iuserRepository;

    public UpdateUserView(IUserRepository iuserRepository)
    {
        this.iuserRepository = iuserRepository;
    }

    public async Task updateUser()
    {
        Console.WriteLine("Please enter the User ID you want to update:");
        string? input = Console.ReadLine();
        if (int.TryParse(input, out int userId))
        {
            var user = await iuserRepository.GetSingleAsync(userId); //Finder brugeren med pågældende id
           
            Console.WriteLine($"Current username: {user.username}"); 
            Console.WriteLine("Please enter a new Username:");
            string? newUsername = Console.ReadLine(); 
            
            user.username = newUsername;                              //Overskriver det gamle username med det nye
            await iuserRepository.UpdateAsync(user);                 //Kalder update med det nye brugernavn
            Console.WriteLine("User updated successfully");
        }
    }
    
}