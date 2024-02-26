namespace Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public bool IsValid()
    {
        return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Email);

    }


}
