namespace Entitities;

public class Comment
{
    public int Id { get; set; }
    public string body { get; set; }
    public int UserId { get; set; }

    public string ToString()
    {
        return $"Id: {Id}, body: {body}, UserId: {UserId}";
    }
}