using DTO;
using Entitities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController: ControllerBase
{
    private readonly IPostRepository postRepo;
    private readonly ICommentRepository commentRepo;

    public PostsController(IPostRepository postRepo, ICommentRepository commentRepo)
    {
        this.postRepo = postRepo;
        this.commentRepo = commentRepo;
    }

    [HttpPost]
    public async Task<ActionResult<CreateUpdatePostDto>> CreatePost([FromBody] CreateUpdatePostDto request)
    {
        try
        {
            Post post = new(request.Title, request.body, request.UserId);
            Post created = await postRepo.AddAsync(post);
            PostDto dto = new()
            {
                Id = created.Id,
                Title = created.Title,
                body = created.body,
                UserId = created.UserId
            };
            return Created($"posts/{dto.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500,e.Message);
        }
    }

}