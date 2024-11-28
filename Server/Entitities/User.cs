namespace Entitities;

public class User
{
   // private User user;
    public int Id { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    
    // Navigation properties
    public List<Post>? Posts { get; set; } = []; // En User kan have mange posts
    public List<Comment>? Comments { get; set; } = []; // En User kan have mange kommentare

    public User(String username, String password)
    {
        this.username = username;
        this.password = password;
    }
    public string ToString()
    {
        string Maskedpassword = new string('*', password.Length); //udskriver password som ***
        return $"User ID: {Id} Username: {username}, Password: {Maskedpassword}";
    }

    private User(){} //For EFC
}