namespace Entitities;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string body { get; set; }
    // Foreign Key
    public int UserId { get; set; }
    

    // Navigation Properties
    public User? User { get; set; } // Hvert post tilhører en user
    public List<Comment>? Comments { get; set; } = []; // En post kan have mange comments

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
    
    private Post(){} // For EFC
}
