namespace DTO;

public class CommentDto
{
    public int Id { get; set; }
    public string body { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
}