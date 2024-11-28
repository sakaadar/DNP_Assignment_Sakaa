using Entitities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository: IUserRepository
{
    private List<User> users;

    public UserInMemoryRepository()
    {
        // users = new List<User>()
        // {
        //     new User() { Id = 1, username = "Sakaadar" },
        //     new User() { Id = 2, username = "FuIzzy" },
        //     new User() { Id = 3, username = "Mugaga" },
        // };
    }
    public Task<User> AddAsync(User user)
    {
        user.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1;
        users.Add(user);
        return Task.FromResult(user);
    }

    public Task UpdateAsync(User user)
    {
        User? userToUpdate = users.SingleOrDefault(u=>u.Id==user.Id);
        if (userToUpdate is null)
        {
            throw new InvalidOperationException("User not found");
        }
        users.Remove(userToUpdate);
        users.Add(user);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int Id)
    {
        User? userToDelete = users.SingleOrDefault(u=>u.Id== Id);
        if (userToDelete is null)
        {
            throw new InvalidOperationException("User does not exists");
        }
        users.Remove(userToDelete);
        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int Id)
    {
        User? userToGet = users.SingleOrDefault(u=>u.Id==Id);
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

    public Task<User?> FindByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }
}