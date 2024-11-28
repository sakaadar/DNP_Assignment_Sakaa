namespace Entitities;

public class Comment
{
    public int Id { get; set; }
    public string body { get; set; }
    public int UserId { get; set; } //Foreign key User
    public int PostId { get; set; } //Foreign key Post
    
    public User User { get; set; } = null!; // Reference navigation property
    public Post Post { get; set; } = null!; // Reference navigation property
   

    public string ToString()
    {
        return $"Id: {Id}, body: {body}, UserId: {UserId}";
    }
    public Comment(){}//For EFC
}