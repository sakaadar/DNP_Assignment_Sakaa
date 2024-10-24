namespace DTO;

public class CreateUpdateCommentDto
{
    public string body { get; set; }
    public int userId { get; set; }
    public int postId { get; set; }
}