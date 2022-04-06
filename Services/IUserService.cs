using Base.Models;

namespace Base.Services;

public interface IUserService
{
    public User? GetById(long id);
    public void DeleteById(long id);
}
