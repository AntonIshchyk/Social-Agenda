public class Admin
{
    public Guid Id { get; set; }
    public string Username { get; }
    public string Password { get; set; }
    public string Email { get; }
    public DateTime LastLogIn { get; set; }
    public DateTime LastLogOut { get; set; }
    public bool LoggedIn { get; set; }

    public Admin(string username, string password, string email)
    {
        Id = Guid.NewGuid();
        Username = username;
        Password = password;
        Email = email;
    }
}