using Base.Models;

namespace Base.Services;

public interface IUserService
{
    public UserModel? GetById(long id);
    public void DeleteById(long id);
}
