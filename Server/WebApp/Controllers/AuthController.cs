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

        // Valider brugerens password (forudsat at det også er en del af `request`)
        if (user.password != request.Password)
        {
            return Unauthorized("Invalid password");
        }
        // Konverter brugeren til en DTO (uden følsomme oplysninger) og returner den
        var userDto = new UserDto
        {
            Id = user.id,
            Username = user.username,
            // Andre nødvendige felter, men uden password
        };

        return Ok(userDto);
    }
}