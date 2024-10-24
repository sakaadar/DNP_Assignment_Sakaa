namespace Entitities;

public class User
{
   // private User user;
    public string username { get; set; }
    public string password { get; set; }
    public int id { get; set; }

    public User(String username, String password)
    {
        this.username = username;
        this.password = password;
    }
    public string ToString()
    {
        string Maskedpassword = new string('*', password.Length); //udskriver password som ***
        return $"User ID: {id} Username: {username}, Password: {Maskedpassword}";
    }
}