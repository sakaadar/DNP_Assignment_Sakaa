namespace DTO;

public class CreateUpdatePostDto
{
    public required string Title { get; set; }
    public required string body { get; set; }
    public required int UserId { get; set; } 
}