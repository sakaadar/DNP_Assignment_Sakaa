using System.Text.Json;
using Entitities;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    private readonly string filePath = "users.json";

    public UserFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }


    public async Task<User> AddAsync(User user)
    {
        string usersAsjson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsjson)!;
        int maxId = users.Count > 0 ? users.Max(u=>u.id) : 1;
        user.id = maxId + 1;
        users.Add(user);
        usersAsjson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsjson);
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        string usersAsjson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsjson)!;
        User? existingUser = users.SingleOrDefault(u => u.id == user.id);
        if (existingUser == null)
        {
            throw new NullReferenceException("User Could not be found");
        }
        users.Remove(existingUser);
        users.Add((user));
        usersAsjson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsjson);
    }

    public async Task DeleteAsync(int id)
    {
        string usersAsjson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsjson)!;
        User? userToDelete = users.SingleOrDefault(u => u.id == id);
        if (userToDelete is null)
        {
            throw new NullReferenceException("User Could not be found");
        }
        users.Remove(userToDelete);
        usersAsjson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsjson);
    }

    public async Task<User> GetSingleAsync(int id)
    {
        string usersAsjson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsjson)!;
        User? singleUser = users.SingleOrDefault(u => u.id == id);
        if (singleUser is null)
        {
            throw new NullReferenceException("User does not exist");
        }
        return singleUser;
    }

    public IQueryable<User> GetMany()
    {
        string usersAsjson = File.ReadAllTextAsync(filePath).Result;
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsjson)!;
        return users.AsQueryable();
    }

    public async Task<bool> IsUsernameTakenAsync(string username)
    {
       string UsersAsjson = await File.ReadAllTextAsync(filePath);
       List<User> users = JsonSerializer.Deserialize<List<User>>(UsersAsjson)!;
       bool isTaken = users.Any(u => u.username == username);
       return isTaken;
    }
    public async Task<User?> FindByUsernameAsync(string username)
    {
        // Læs brugerdata fra filen
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;

        // Find og returner brugeren med det givne brugernavn
        var user = users.FirstOrDefault(u => u.username == username);
        return user; // Returnerer null, hvis brugeren ikke findes
    }
}