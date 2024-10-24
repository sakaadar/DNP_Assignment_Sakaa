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

    [HttpGet]
    public async Task<ActionResult<List<PostDto>>> GetPosts([FromQuery] string? Title, [FromQuery] int? UserId)
    {
        try
        {
            var posts = postRepo.GetMany();
            if (Title != null)
            {
                posts = posts.Where(p => p.Title == Title);
            }

            if (UserId.HasValue)
            {
                posts = posts.Where(p => p.UserId == UserId);
            }

            List<PostDto> postDtos = posts.Select(p => new PostDto
            {
                Id = p.Id, Title = p.Title, body = p.body, UserId = p.UserId
            }).ToList();
            return Ok(postDtos);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostDto>> GetSinglePost(int id)
    {
        try
        {
            var post = await postRepo.GetSingleAsync(id);
            return Ok(new PostDto
            {
                Id = post.Id, Title = post.Title, body = post.body,
                UserId = post.UserId
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
        
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CreateUpdatePostDto>> UpdatePost(int id, [FromBody] CreateUpdatePostDto request)
    {
        try
        {
            var postToUpdate = await postRepo.GetSingleAsync(id);
            postToUpdate.Title = request.Title;
            postToUpdate.body = request.body;
            postToUpdate.UserId = request.UserId;

            await postRepo.UpdateAsync(postToUpdate);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<PostDto>> DeletePost(int id)
    {
        await postRepo.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("{id}/comments")]
    public async Task<ActionResult<List<CreateUpdateCommentDto>>> AddCommentToPost(
        int id, [FromBody] CreateUpdateCommentDto request)
    {
        try
        {
            var posts = await postRepo.GetSingleAsync(id);
            if (posts == null)
            {
                return BadRequest("Post does not exist");
            }

            var comment = new Comment
            {
                body = request.body,
                PostId = request.postId,
                UserId = request.userId
            };

            await commentRepo.AddAsync(comment);

            var comments =
                await commentRepo
                    .GetCommentsForPostAsync(
                        id); //Hent alle kommentarer for dette specifikke post

            //Konverter til dto
            var commentsdto = comments.Select(c => new CommentDto
            {
                Id = c.Id,
                body = c.body,
                UserId = c.UserId,
                PostId = c.PostId
            }).ToList();

            return Ok(commentsdto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

}