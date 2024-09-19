namespace Entitities;

public class User
{
   // private User user;
    public string username { get; set; }
    public string password { get; set; }
    public int id { get; set; }

    public string ToString()
    {
        return $"User ID: {id} Username:{username}";
    }
}