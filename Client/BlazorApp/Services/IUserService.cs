using System.Threading.Tasks;
using DTO;

namespace BlazorApp.Services;

public interface IUserService
{
    public Task<UserDto> AddUserAsync(CreateUserDto request);
    public Task UpdateUserAsync(int id, CreateUserDto request);
    public Task<UserDto> GetUsersAsync(int id);
    public Task DeleteUserAsync(int id);
    public Task<List<UserDto>> GetAllUsersAsync();
}