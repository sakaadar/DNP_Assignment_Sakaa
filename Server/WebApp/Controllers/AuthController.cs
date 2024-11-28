using DTO;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository userRepository;

    public AuthController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var user = await userRepository.FindByUsernameAsync(request.Username);
        
        if (user == null)
        {
            return Unauthorized("User not found");
        }

        // Valider brugerens password 
        if (user.password != request.Password)
        {
            return Unauthorized("Invalid password");
        }
        // Konverter brugeren til en DTO 
        var userDto = new UserDto
        {
            Id = user.Id,
            Username = user.username,
            // Andre nødvendige felter, men uden password
        };

        return Ok(userDto);
    }
}