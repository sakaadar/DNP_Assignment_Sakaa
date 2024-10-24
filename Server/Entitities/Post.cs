namespace Entitities;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string body { get; set; }
    public int UserId { get; set; }

    public Post(string Title, string body, int UserId)
    {
        this.Title = Title;
        this.body = body;
        this.UserId = UserId;
    }
    public string ToString()
    {
        return $"UserId: {UserId} | Title: {Title} | Body: {body}";
    }
}
