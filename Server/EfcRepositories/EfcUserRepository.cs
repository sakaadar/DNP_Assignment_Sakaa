using Entitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcUserRepository : IUserRepository
{
    private readonly AppContext ctx;

    public EfcUserRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }
    public async Task<User> AddAsync(User user)
    {
        EntityEntry<User> entry = ctx.Users.Add(user);
        await ctx.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task UpdateAsync(User user)
    {
        if (!(await ctx.Users.AnyAsync(u => u.Id == user.Id)))
        {
            throw new InvalidOperationException("User does not exist");
        }
        ctx.Users.Update(user);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
       User? existingUser = ctx.Users.FirstOrDefault(u => u.Id == id);
       if (existingUser == null)
       {
           throw new InvalidOperationException("User does not exist");
       }
       ctx.Users.Remove(existingUser);
       await ctx.SaveChangesAsync();
    }

    public async Task<User> GetSingleAsync(int id)
    {
        User? user = await ctx.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user is null)
        {
            throw new InvalidOperationException("User does not exist");
        }
        return user;
    }

    public IQueryable<User> GetMany()
    {
       return ctx.Users.AsQueryable();
    }

    public async Task<bool> IsUsernameTakenAsync(string username)
    {
        return await ctx.Users.AnyAsync(u => u.username == username);
    }

    public async Task<User?> FindByUsernameAsync(string username)
    {
        return await ctx.Users.FirstOrDefaultAsync(u => u.username == username);
    }
}