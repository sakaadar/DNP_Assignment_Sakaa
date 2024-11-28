using DTO;
using Entitities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace WebApp.Controllers;
[ApiController]
[Route("[controller]")]
public class UserController: ControllerBase
{
   private readonly IUserRepository userRepo;

   public UserController(IUserRepository userRepo)
   {
      this.userRepo = userRepo;
   }

   [HttpPost]
   public async Task<ActionResult<UserDto>> AddUser([FromBody] CreateUserDto request)
   {
      try
      {
         await VerifyUserNameIsAvailableAsync(request.UserName);
         User user = new(request.UserName, request.Password);
         User created = await userRepo.AddAsync(user);
         UserDto dto = new()
         {
            Id = created.Id,
            Username = created.username
         };
         return Created($"/User/{dto.Id}", created);
      }
      catch (Exception e)
      {
         Console.WriteLine(e);
         return StatusCode(500,e.Message);
      }
   }
   
   [HttpGet]
   public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers([FromQuery] string? usernameContains)
   {
      try
      {
         var usersQuery = userRepo.GetMany();
         if (!string.IsNullOrEmpty(usernameContains)) //tjekkes at der ikke er blevet sendt en tom streng
         {
            usersQuery = usersQuery.Where(u => u.username.Contains(usernameContains)); //Filtrering users om den indeholder den streng der blev sendt med.
         }
         var dto = await usersQuery.Select(u => new UserDto{Id = u.Id, Username = u.username}).ToListAsync(); //Konvertere hver brugere til en DTO
         return Ok(dto);
      }
      catch (Exception e)
      {
         Console.WriteLine(e);
         return StatusCode(500, e.Message);
      }
   }

   [HttpGet("{id}")]
   public async Task<ActionResult<UserDto>> GetUser(int id)
   {
      try
      {
         var user = await userRepo.GetSingleAsync(id);
         if (user == null)
         {
            return NotFound("User not found");
         }

         return Ok(new UserDto { Id = user.Id, Username = user.username });
      }
      catch (Exception e)
      {
         Console.WriteLine(e);
         return StatusCode(500, e.Message);
      }
   }

   [HttpPut("{id}")]
   public async Task<ActionResult<UserDto>> UpdateUser(int id, [FromBody] CreateUserDto dto)
   {
      try
      {
         var user = await userRepo.GetSingleAsync(id);
         if (user == null)
         {
            return BadRequest();
         }

         user.username = dto.UserName;
         user.password = dto.Password;
         await userRepo.UpdateAsync(user);
         return NoContent();

      }
      catch (Exception e)
      {
         Console.WriteLine(e);
         return StatusCode(500, e.Message);
      }
   }

   [HttpDelete("{id}")]
   public async Task<IActionResult> DeleteUser(int id)
   {
      await userRepo.DeleteAsync(id);
      return NoContent();
   }

   private async Task VerifyUserNameIsAvailableAsync(string userName)
   {
      var existinguser =  await userRepo.IsUsernameTakenAsync(userName);
      if (existinguser)
      {
         throw new Exception($"User {userName} is already taken");
      }
   }

}