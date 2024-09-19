using RepositoryContracts;

namespace CLI.UI.Utility;

public class InputValidator
{
    private readonly IUserRepository userRepository;

    public InputValidator(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<int> getValidUserId()
    {
        int userId = 0;
        bool IsvalidUser = false;
        while (!IsvalidUser)
        {
            Console.WriteLine("Enter a user ID: ");
            var intput = Console.ReadLine();
            if (int.TryParse(intput, out userId))
            {
                var user = await userRepository.GetSingleAsync(userId);
                if (user != null)
                {
                    IsvalidUser = true;
                }
                else
                {
                    Console.WriteLine("Invalid User ID, try again");
                }
            }
            else
            {
                Console.WriteLine("Enter a valid number");
            }
        }
        return userId;
    }
}   
