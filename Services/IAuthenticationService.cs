namespace Base.Services;

public interface IAuthenticationService
{
    public (bool success, string content) Register(string username, string password);
    public (bool success, string token) Login(string username, string password);
}
