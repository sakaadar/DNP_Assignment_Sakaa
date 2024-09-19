namespace Entitities;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string body { get; set; }
    public int UserId { get; set; }

    public string ToString()
    {
        return $"UserId: {UserId} | Title: {Title} | Body: {body}";
    }
}
