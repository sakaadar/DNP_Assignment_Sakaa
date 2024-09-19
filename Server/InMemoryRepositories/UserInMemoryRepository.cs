using Entitities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository: IUserRepository
{
    private List<User> users;

    public UserInMemoryRepository()
    {
        users = new List<User>()
        {
            new User() { id = 1, username = "Sakaadar" },
            new User() { id = 2, username = "FuIzzy" },
            new User() { id = 3, username = "Mugaga" },
        };
    }
    public Task<User> AddAsync(User user)
    {
        user.id = users.Any() ? users.Max(u => u.id) + 1 : 1;
        users.Add(user);
        return Task.FromResult(user);
    }

    public Task UpdateAsync(User user)
    {
        User? userToUpdate = users.FirstOrDefault(u=>u.id==user.id);
        if (userToUpdate is null)
        {
            throw new InvalidOperationException("User not found");
        }
        users.Remove(userToUpdate);
        users.Add(user);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        User? userToDelete = users.FirstOrDefault(u=>u.id== id);
        if (userToDelete is null)
        {
            throw new InvalidOperationException("User does not exists");
        }
        users.Remove(userToDelete);
        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int id)
    {
        User? userToGet = users.FirstOrDefault(u=>u.id==id);
        if (userToGet is null)
        {
            throw new InvalidOperationException("User not found");
        }
        return Task.FromResult(userToGet);
    }

    public IQueryable<User> GetMany()
    {
        return users.AsQueryable();
    }

    public Task<bool> IsUsernameTakenAsync(string username)
    {
        bool isTaken = users.Any(u=>u.username.Equals(username, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(isTaken);
    }
}