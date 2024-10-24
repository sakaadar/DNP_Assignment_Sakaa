using DTO;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController: ControllerBase
{
    private readonly ICommentRepository commentRepo;

    public CommentsController(ICommentRepository commentRepo)
    {
        this.commentRepo = commentRepo;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetComments([FromQuery] int? postId,[FromQuery] int? userId)
    {
        try
        {
            var comments = commentRepo.GetMany();

            if (postId.HasValue)
            {
                comments = comments.Where(c => c.PostId == postId.Value);
            }

            if (userId.HasValue)
            {
                comments = comments.Where(c => c.UserId == userId.Value);
            }
            List<CommentDto> commentDtos = comments.Select(c=>new CommentDto{body=c.body,Id=c.Id,UserId=c.UserId}).ToList();
            return Ok(commentDtos);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500,e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CommentDto>> GetComment(int id)
    {
        try
        {
            var comment = await commentRepo.GetSingleAsync(id);
            return Ok(new CommentDto{body=comment.body,Id=comment.Id,UserId=comment.UserId,PostId=comment.PostId});

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CreateUpdateCommentDto>> UpdateComment(int id, CreateUpdateCommentDto comment)
    {
        try
        {
            var commentToUpdate = await commentRepo.GetSingleAsync(id);
            commentToUpdate.body = comment.body;
            commentToUpdate.PostId = comment.postId;
            commentToUpdate.UserId = comment.userId;
            
            await commentRepo.UpdateAsync(commentToUpdate);
            return NoContent();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }   
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<CommentDto>> DeleteComment(int id)
    {
       await commentRepo.DeleteAsync(id);
       return NoContent();
    }
}